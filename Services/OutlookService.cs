using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using BettyMailZoom.Models;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace BettyMailZoom.Services
{
    public class FolderInfo
    {
        public string FolderPath { get; set; }
        public string FolderName { get; set; }
        public string StoreName { get; set; }
        public string StoreId { get; set; }
        public string EntryId { get; set; }
        public int ItemCount { get; set; }
        public bool IsSelected { get; set; } = true;
    }

    public class OutlookService
    {
        private const string PR_SMTP_ADDRESS = "http://schemas.microsoft.com/mapi/proptag/0x39FE001E";

        public static bool IsOutlookInstalled()
        {
            try
            {
                Type outlookType = Type.GetTypeFromProgID("Outlook.Application");
                return outlookType != null;
            }
            catch
            {
                return false;
            }
        }

        private Outlook.Application GetOutlookApp()
        {
            try
            {
                // Try attaching to active instance first
                return (Outlook.Application)Marshal.GetActiveObject("Outlook.Application");
            }
            catch
            {
                // Create new instance
                try
                {
                    return new Outlook.Application();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to connect to Microsoft Outlook. Please ensure Outlook is installed and configured.", ex);
                }
            }
        }

        public List<FolderInfo> GetFolders()
        {
            var result = new List<FolderInfo>();
            Outlook.Application app = null;
            Outlook.NameSpace ns = null;
            Outlook.Stores stores = null;

            try
            {
                app = GetOutlookApp();
                ns = app.GetNamespace("MAPI");
                stores = ns.Stores;

                for (int i = 1; i <= stores.Count; i++)
                {
                    Outlook.Store store = null;
                    Outlook.MAPIFolder rootFolder = null;
                    try
                    {
                        store = stores[i];
                        string storeName = store.DisplayName;
                        string storeId = store.StoreID;
                        rootFolder = store.GetRootFolder();

                        EnumerateFolders(rootFolder, storeName, storeId, result);
                    }
                    catch { }
                    finally
                    {
                        ReleaseCom(rootFolder);
                        ReleaseCom(store);
                    }
                }
            }
            finally
            {
                ReleaseCom(stores);
                ReleaseCom(ns);
                ReleaseCom(app);
            }

            return result;
        }

        private void EnumerateFolders(Outlook.MAPIFolder parentFolder, string storeName, string storeId, List<FolderInfo> list)
        {
            if (parentFolder == null) return;

            Outlook.Folders subFolders = null;
            try
            {
                // We only care about mail folders (OlDefaultFolders / olFolderInbox, etc. or folder holding mail)
                if (parentFolder.DefaultItemType == Outlook.OlItemType.olMailItem)
                {
                    int count = 0;
                    try { count = parentFolder.Items.Count; } catch { }

                    list.Add(new FolderInfo
                    {
                        FolderPath = parentFolder.FolderPath,
                        FolderName = parentFolder.Name,
                        StoreName = storeName,
                        StoreId = storeId,
                        EntryId = parentFolder.EntryID,
                        ItemCount = count,
                        // Default to true for standard mail folders, uncheck Junk/Deleted
                        IsSelected = !parentFolder.Name.Equals("Deleted Items", StringComparison.OrdinalIgnoreCase) &&
                                     !parentFolder.Name.Equals("Junk Email", StringComparison.OrdinalIgnoreCase) &&
                                     !parentFolder.Name.Equals("Trash", StringComparison.OrdinalIgnoreCase) &&
                                     !parentFolder.Name.Equals("Sync Issues", StringComparison.OrdinalIgnoreCase)
                    });
                }

                subFolders = parentFolder.Folders;
                for (int i = 1; i <= subFolders.Count; i++)
                {
                    Outlook.MAPIFolder child = null;
                    try
                    {
                        child = subFolders[i];
                        EnumerateFolders(child, storeName, storeId, list);
                    }
                    catch { }
                    finally
                    {
                        ReleaseCom(child);
                    }
                }
            }
            finally
            {
                ReleaseCom(subFolders);
            }
        }

        public void FetchEmailsFromFolder(
            string folderEntryId,
            string storeId,
            DateTime? modifiedSince,
            bool indexBody,
            int maxBodyLength,
            Action<List<EmailItemModel>> onBatchReady,
            Func<bool> checkCancelled,
            Action<string, int, int> onFolderProgress)
        {
            Outlook.Application app = null;
            Outlook.NameSpace ns = null;
            Outlook.MAPIFolder folder = null;
            Outlook.Items items = null;

            try
            {
                app = GetOutlookApp();
                ns = app.GetNamespace("MAPI");

                try
                {
                    folder = ns.GetFolderFromID(folderEntryId, storeId);
                }
                catch
                {
                    return; // Folder might have been deleted/moved
                }

                if (folder == null) return;

                string folderPath = folder.FolderPath;
                string storeName = "";
                try { storeName = folder.Store.DisplayName; } catch { }

                items = folder.Items;
                items.Sort("[ReceivedTime]", true); // Sort descending

                // If incremental sync, restrict query
                if (modifiedSince.HasValue)
                {
                    try
                    {
                        string filter = $"[LastModificationTime] >= '{modifiedSince.Value:g}'";
                        var filteredItems = items.Restrict(filter);
                        ReleaseCom(items);
                        items = filteredItems;
                    }
                    catch
                    {
                        // Fallback to manual filter if Restrict fails
                    }
                }

                int totalCount = 0;
                try { totalCount = items.Count; } catch { }

                var batch = new List<EmailItemModel>();
                int batchSize = 100;
                int processed = 0;

                for (int i = 1; i <= totalCount; i++)
                {
                    if (checkCancelled != null && checkCancelled())
                    {
                        break;
                    }

                    object rawItem = null;
                    try
                    {
                        rawItem = items[i];
                        if (rawItem is Outlook.MailItem mail)
                        {
                            var model = ExtractEmailData(mail, folderPath, storeName, storeId, indexBody, maxBodyLength);
                            if (model != null)
                            {
                                batch.Add(model);
                            }
                        }
                    }
                    catch { }
                    finally
                    {
                        ReleaseCom(rawItem);
                    }

                    processed++;

                    if (processed % 25 == 0 || processed == totalCount)
                    {
                        onFolderProgress?.Invoke(folderPath, processed, totalCount);
                    }

                    if (batch.Count >= batchSize)
                    {
                        onBatchReady?.Invoke(batch);
                        batch.Clear();
                    }
                }

                if (batch.Count > 0)
                {
                    onBatchReady?.Invoke(batch);
                    batch.Clear();
                }
            }
            finally
            {
                ReleaseCom(items);
                ReleaseCom(folder);
                ReleaseCom(ns);
                ReleaseCom(app);
            }
        }

        private EmailItemModel ExtractEmailData(Outlook.MailItem mail, string folderPath, string storeName, string storeId, bool indexBody, int maxBodyLength)
        {
            if (mail == null) return null;

            try
            {
                var model = new EmailItemModel
                {
                    EntryId = mail.EntryID ?? Guid.NewGuid().ToString(),
                    StoreId = storeId,
                    Subject = mail.Subject ?? "(No Subject)",
                    ReceivedTime = mail.ReceivedTime,
                    Importance = (int)mail.Importance,
                    Size = mail.Size,
                    FolderPath = folderPath,
                    StoreName = storeName,
                    IsRead = !mail.UnRead,
                    Categories = mail.Categories ?? "",
                    LastModifiedTime = mail.LastModificationTime
                };

                // Extract Sender
                string senderName = "";
                string senderEmail = "";
                try
                {
                    senderName = mail.SenderName ?? "";
                    if (mail.SenderEmailType == "EX")
                    {
                        try
                        {
                            var sender = mail.Sender;
                            if (sender != null)
                            {
                                var exUser = sender.GetExchangeUser();
                                if (exUser != null)
                                {
                                    senderEmail = exUser.PrimarySmtpAddress ?? "";
                                    ReleaseCom(exUser);
                                }
                                ReleaseCom(sender);
                            }
                        }
                        catch { }

                        if (string.IsNullOrWhiteSpace(senderEmail))
                        {
                            try
                            {
                                var propAccessor = mail.PropertyAccessor;
                                senderEmail = (string)propAccessor.GetProperty(PR_SMTP_ADDRESS);
                                ReleaseCom(propAccessor);
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        senderEmail = mail.SenderEmailAddress ?? "";
                    }
                }
                catch
                {
                    senderEmail = mail.SenderEmailAddress ?? "";
                }

                model.SenderName = senderName;
                model.SenderEmail = senderEmail;

                // Recipients
                try { model.ToAddresses = mail.To ?? ""; } catch { }
                try { model.CcAddresses = mail.CC ?? ""; } catch { }

                // Attachments
                try
                {
                    var attachments = mail.Attachments;
                    if (attachments != null && attachments.Count > 0)
                    {
                        model.HasAttachments = true;
                        model.AttachmentCount = attachments.Count;
                        var attNames = new List<string>();
                        for (int a = 1; a <= attachments.Count; a++)
                        {
                            Outlook.Attachment att = null;
                            try
                            {
                                att = attachments[a];
                                if (!string.IsNullOrWhiteSpace(att.FileName))
                                {
                                    attNames.Add(att.FileName);
                                }
                            }
                            catch { }
                            finally
                            {
                                ReleaseCom(att);
                            }
                        }
                        model.AttachmentNames = string.Join("; ", attNames);
                        ReleaseCom(attachments);
                    }
                    else
                    {
                        model.HasAttachments = false;
                        model.AttachmentNames = "";
                        model.AttachmentCount = 0;
                    }
                }
                catch
                {
                    model.HasAttachments = false;
                    model.AttachmentNames = "";
                }

                // Body content
                if (indexBody)
                {
                    try
                    {
                        string body = mail.Body ?? "";
                        if (body.Length > maxBodyLength)
                        {
                            body = body.Substring(0, maxBodyLength);
                        }
                        model.BodyText = body;

                        // Create clean snippet (first 250 chars without excess newlines)
                        string cleanSnippet = body.Replace("\r\n", " ").Replace("\n", " ").Trim();
                        if (cleanSnippet.Length > 250)
                        {
                            cleanSnippet = cleanSnippet.Substring(0, 250) + "...";
                        }
                        model.BodySnippet = cleanSnippet;
                    }
                    catch
                    {
                        model.BodyText = "";
                        model.BodySnippet = "";
                    }

                    try
                    {
                        string html = mail.HTMLBody ?? "";
                        // If HTML body is within reasonable length (e.g. 500KB), store it for rich preview
                        if (html.Length <= 500000)
                        {
                            model.BodyHtml = html;
                        }
                        else
                        {
                            model.BodyHtml = null;
                        }
                    }
                    catch
                    {
                        model.BodyHtml = null;
                    }
                }

                return model;
            }
            catch
            {
                return null;
            }
        }

        public bool OpenEmail(string entryId, string storeId)
        {
            if (string.IsNullOrWhiteSpace(entryId)) return false;

            Outlook.Application app = null;
            Outlook.NameSpace ns = null;
            object item = null;

            try
            {
                app = GetOutlookApp();
                ns = app.GetNamespace("MAPI");

                if (!string.IsNullOrWhiteSpace(storeId))
                {
                    item = ns.GetItemFromID(entryId, storeId);
                }
                else
                {
                    item = ns.GetItemFromID(entryId);
                }

                if (item is Outlook.MailItem mail)
                {
                    mail.Display(false); // Non-modal display in native Outlook
                    return true;
                }
                else if (item is Outlook.MeetingItem meeting)
                {
                    meeting.Display(false);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Could not open email in Outlook: {ex.Message}", ex);
            }
            finally
            {
                ReleaseCom(item);
                ReleaseCom(ns);
                ReleaseCom(app);
            }

            return false;
        }

        public bool DeleteEmail(string entryId, string storeId)
        {
            if (string.IsNullOrWhiteSpace(entryId)) return false;

            Outlook.Application app = null;
            Outlook.NameSpace ns = null;
            object item = null;

            try
            {
                app = GetOutlookApp();
                ns = app.GetNamespace("MAPI");

                if (!string.IsNullOrWhiteSpace(storeId))
                {
                    item = ns.GetItemFromID(entryId, storeId);
                }
                else
                {
                    item = ns.GetItemFromID(entryId);
                }

                if (item is Outlook.MailItem mail)
                {
                    mail.Delete(); // Standard soft delete / move to Deleted Items
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Could not delete email from Outlook: {ex.Message}", ex);
            }
            finally
            {
                ReleaseCom(item);
                ReleaseCom(ns);
                ReleaseCom(app);
            }

            return false;
        }

        public bool SaveAttachment(string entryId, string storeId, string attachmentName, string savePath)
        {
            if (string.IsNullOrWhiteSpace(entryId) || string.IsNullOrWhiteSpace(attachmentName)) return false;

            Outlook.Application app = null;
            Outlook.NameSpace ns = null;
            object item = null;
            Outlook.Attachments attachments = null;

            try
            {
                app = GetOutlookApp();
                ns = app.GetNamespace("MAPI");

                if (!string.IsNullOrWhiteSpace(storeId))
                {
                    item = ns.GetItemFromID(entryId, storeId);
                }
                else
                {
                    item = ns.GetItemFromID(entryId);
                }

                if (item is Outlook.MailItem mail)
                {
                    attachments = mail.Attachments;
                    for (int i = 1; i <= attachments.Count; i++)
                    {
                        Outlook.Attachment att = null;
                        try
                        {
                            att = attachments[i];
                            if (string.Equals(att.FileName, attachmentName, StringComparison.OrdinalIgnoreCase))
                            {
                                att.SaveAsFile(savePath);
                                return true;
                            }
                        }
                        finally
                        {
                            ReleaseCom(att);
                        }
                    }
                }
            }
            finally
            {
                ReleaseCom(attachments);
                ReleaseCom(item);
                ReleaseCom(ns);
                ReleaseCom(app);
            }

            return false;
        }

        public string GetFullHtmlBody(string entryId, string storeId)
        {
            if (string.IsNullOrWhiteSpace(entryId)) return null;

            Outlook.Application app = null;
            Outlook.NameSpace ns = null;
            object item = null;

            try
            {
                app = GetOutlookApp();
                ns = app.GetNamespace("MAPI");

                if (!string.IsNullOrWhiteSpace(storeId))
                {
                    item = ns.GetItemFromID(entryId, storeId);
                }
                else
                {
                    item = ns.GetItemFromID(entryId);
                }

                if (item is Outlook.MailItem mail)
                {
                    return mail.HTMLBody ?? mail.Body ?? "";
                }
            }
            catch { }
            finally
            {
                ReleaseCom(item);
                ReleaseCom(ns);
                ReleaseCom(app);
            }

            return null;
        }

        private static void ReleaseCom(object obj)
        {
            if (obj != null && Marshal.IsComObject(obj))
            {
                try
                {
                    Marshal.ReleaseComObject(obj);
                }
                catch { }
            }
        }
    }
}
