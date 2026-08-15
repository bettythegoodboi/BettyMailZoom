using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using BettyMailZoom.Models;
using BettyMailZoom.Services;

namespace BettyMailZoom.Tests
{
    public static class TestRunner
    {
        public static void RunTests()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine(" Running BettyMailZoom Search & Engine Unit Tests ");
            Console.WriteLine("==================================================");

            string tempDb = Path.Combine(Path.GetTempPath(), $"test_mail_{Guid.NewGuid():N}.db");
            try
            {
                using (var db = new SearchIndexDatabase(tempDb))
                {
                    // 1. Test Seed Data
                    Console.WriteLine("\n[1] Seeding test email data...");
                    var testData = new List<EmailItemModel>
                    {
                        new EmailItemModel
                        {
                            EntryId = "E001",
                            StoreId = "S1",
                            Subject = "Urgent: Q3 Financial Report & Budget Review",
                            SenderName = "Alice Finance",
                            SenderEmail = "alice@company.com",
                            ToAddresses = "team@company.com",
                            ReceivedTime = DateTime.Now.AddDays(-2),
                            HasAttachments = true,
                            AttachmentNames = "q3_budget.xlsx; financial_summary.pdf",
                            AttachmentCount = 2,
                            Importance = 2, // High
                            Size = 45000,
                            BodyText = "Please find attached the Q3 financial report. We need to approve the capital expenditure budget by Friday.",
                            FolderPath = "\\\\Mailbox - John\\Inbox",
                            IsRead = false,
                            LastModifiedTime = DateTime.Now.AddDays(-2)
                        },
                        new EmailItemModel
                        {
                            EntryId = "E002",
                            StoreId = "S1",
                            Subject = "Weekly Team Lunch & Social Gathering",
                            SenderName = "Bob Social",
                            SenderEmail = "bob@company.com",
                            ToAddresses = "all@company.com",
                            ReceivedTime = DateTime.Now.AddDays(-5),
                            HasAttachments = false,
                            Importance = 1, // Normal
                            Size = 12000,
                            BodyText = "Hey everyone, lunch will be at Pizza Plaza this Friday at 12:30 PM. Let me know if you are coming!",
                            FolderPath = "\\\\Mailbox - John\\Inbox",
                            IsRead = true,
                            LastModifiedTime = DateTime.Now.AddDays(-5)
                        },
                        new EmailItemModel
                        {
                            EntryId = "E003",
                            StoreId = "S1",
                            Subject = "Monthly Server Security Newsletter (Spam / Digest)",
                            SenderName = "Security Bot",
                            SenderEmail = "noreply@securitynews.com",
                            ToAddresses = "john@company.com",
                            ReceivedTime = DateTime.Now.AddDays(-10),
                            HasAttachments = false,
                            Importance = 0, // Low
                            Size = 8000,
                            BodyText = "Here is your monthly security newsletter digest. New CVE vulnerabilities reported.",
                            FolderPath = "\\\\Mailbox - John\\Archive",
                            IsRead = true,
                            LastModifiedTime = DateTime.Now.AddDays(-10)
                        },
                        new EmailItemModel
                        {
                            EntryId = "E004",
                            StoreId = "S1",
                            Subject = "Project Architecture Zoom Specs and Roadmap",
                            SenderName = "Charlie Lead",
                            SenderEmail = "charlie@company.com",
                            ToAddresses = "john@company.com",
                            ReceivedTime = DateTime.Now.AddHours(-3),
                            HasAttachments = true,
                            AttachmentNames = "architecture_diagram.png; specs.docx",
                            AttachmentCount = 2,
                            Importance = 2, // High
                            Size = 150000,
                            BodyText = "Let's zoom into the new search engine architecture. The local index will bypass the slow Office 365 cloud search latency.",
                            FolderPath = "\\\\Mailbox - John\\Inbox",
                            IsRead = false,
                            LastModifiedTime = DateTime.Now.AddHours(-3)
                        }
                    };

                    db.UpsertBatch(testData);
                    int totalCount = db.GetTotalEmailCount();
                    Assert(totalCount == 4, $"Total count should be 4, got {totalCount}");
                    Console.WriteLine("-> Seeded 4 emails successfully.");

                    // 2. Test Keyword Search
                    Console.WriteLine("\n[2] Testing Keyword Search 'budget'...");
                    int count;
                    var res1 = db.Search(new SearchQuery { Keyword = "budget" }, out count);
                    Assert(res1.Count == 1 && res1[0].EntryId == "E001", "Keyword search 'budget' failed.");
                    Console.WriteLine($"-> Found {res1.Count} match: '{res1[0].Subject}'");

                    // 3. Test Multi-criteria Filter: Has Attachments + High Importance
                    Console.WriteLine("\n[3] Testing Filters: Has Attachments + High Importance (🔴)...");
                    var res2 = db.Search(new SearchQuery
                    {
                        AttachmentFilter = 1,
                        ImportanceFilter = 2
                    }, out count);
                    Assert(res2.Count == 2, $"Expected 2 matches, got {res2.Count}");
                    Console.WriteLine($"-> Found {res2.Count} matches with attachments and high importance.");

                    // 4. Test Attachment Extension Filter
                    Console.WriteLine("\n[4] Testing Attachment Extension Filter: '.xlsx'...");
                    var res3 = db.Search(new SearchQuery
                    {
                        AttachmentFilter = 1,
                        AttachmentExtension = "xlsx"
                    }, out count);
                    Assert(res3.Count == 1 && res3[0].EntryId == "E001", "Attachment extension filter failed.");
                    Console.WriteLine($"-> Found {res3.Count} match with .xlsx: '{res3[0].Subject}'");

                    // 5. Test Exclude Filter
                    Console.WriteLine("\n[5] Testing Exclude Filter: Exclude 'newsletter'...");
                    var res4 = db.Search(new SearchQuery
                    {
                        ExcludeTerms = "newsletter"
                    }, out count);
                    Assert(res4.Count == 3, $"Expected 3 matches after excluding newsletter, got {res4.Count}");
                    Console.WriteLine($"-> Correctly excluded newsletter item (Result count: {res4.Count}).");

                    // 6. Test Sender Filter
                    Console.WriteLine("\n[6] Testing Sender Filter: 'alice'...");
                    var res5 = db.Search(new SearchQuery
                    {
                        Sender = "alice"
                    }, out count);
                    Assert(res5.Count == 1 && res5[0].SenderName == "Alice Finance", "Sender filter failed.");
                    Console.WriteLine($"-> Found sender: '{res5[0].SenderName}'");

                    // 7. Test Date Range Filter
                    Console.WriteLine("\n[7] Testing Date Range Filter: Last 3 days...");
                    var res6 = db.Search(new SearchQuery
                    {
                        DateFrom = DateTime.Today.AddDays(-3),
                        DateTo = DateTime.Today
                    }, out count);
                    Assert(res6.Count == 2, $"Expected 2 emails in last 3 days, got {res6.Count}");
                    Console.WriteLine($"-> Found {res6.Count} emails in date range.");

                    // 8. Test Unread Filter
                    Console.WriteLine("\n[8] Testing Unread Only Filter...");
                    var res7 = db.Search(new SearchQuery
                    {
                        UnreadOnly = true
                    }, out count);
                    Assert(res7.Count == 2, $"Expected 2 unread emails, got {res7.Count}");
                    Console.WriteLine($"-> Found {res7.Count} unread emails.");

                    // 9. Test Email Deletion
                    Console.WriteLine("\n[9] Testing Delete Email 'E003'...");
                    db.DeleteEmail("E003");
                    int afterDeleteCount = db.GetTotalEmailCount();
                    Assert(afterDeleteCount == 3, $"Expected 3 items after delete, got {afterDeleteCount}");
                    var deletedCheck = db.GetEmailByEntryId("E003");
                    Assert(deletedCheck == null, "Deleted email still found in database.");
                    Console.WriteLine("-> Email deleted from index successfully.");

                    // 10. Performance Benchmark (1000 items)
                    Console.WriteLine("\n[10] Performance Benchmark: Indexing & Querying 1,000 synthetic emails...");
                    var bulkList = new List<EmailItemModel>();
                    var rand = new Random(42);
                    for (int i = 1; i <= 1000; i++)
                    {
                        bulkList.Add(new EmailItemModel
                        {
                            EntryId = $"BULK_{i:D4}",
                            StoreId = "S1",
                            Subject = $"Customer Invoice #{rand.Next(10000, 99999)} - Urgent Order Settlement",
                            SenderName = $"Vendor Rep #{i % 20}",
                            SenderEmail = $"vendor{i % 20}@partner.com",
                            ToAddresses = "billing@company.com",
                            ReceivedTime = DateTime.Now.AddMinutes(-rand.Next(100, 100000)),
                            HasAttachments = i % 3 == 0,
                            AttachmentNames = i % 3 == 0 ? $"invoice_{i}.pdf" : "",
                            AttachmentCount = i % 3 == 0 ? 1 : 0,
                            Importance = i % 5 == 0 ? 2 : 1,
                            Size = rand.Next(5000, 80000),
                            BodyText = $"Hello billing team, please process payment for statement #{i * 123}. Direct any questions to our accounts department.",
                            FolderPath = "\\\\Mailbox - John\\Invoices",
                            IsRead = i % 2 == 0,
                            LastModifiedTime = DateTime.Now
                        });
                    }

                    var swIndex = Stopwatch.StartNew();
                    db.UpsertBatch(bulkList);
                    swIndex.Stop();
                    Console.WriteLine($"-> Indexed 1,000 emails in {swIndex.ElapsedMilliseconds} ms ({(1000.0 / (swIndex.Elapsed.TotalSeconds)):F0} items/sec)!");

                    var swQuery = Stopwatch.StartNew();
                    var searchRes = db.Search(new SearchQuery { Keyword = "statement" }, out count);
                    swQuery.Stop();
                    Console.WriteLine($"-> Sub-second search: Found {searchRes.Count:N0} matches in {swQuery.ElapsedMilliseconds} ms ({swQuery.Elapsed.TotalMilliseconds:F2} ms)!");
                }

                Console.WriteLine("\n==================================================");
                Console.WriteLine("       ALL UNIT & PERFORMANCE TESTS PASSED!       ");
                Console.WriteLine("==================================================");
            }
            finally
            {
                try
                {
                    if (File.Exists(tempDb))
                    {
                        File.Delete(tempDb);
                    }
                }
                catch { }
            }
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                throw new Exception($"Assertion Failed: {message}");
            }
        }
    }
}
