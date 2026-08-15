# BettyMailZoom ⚡
**Ultra-Fast Local Email Search Helper for Microsoft Outlook 365 on Windows 10**

Built with **.NET Framework 4.8 Windows Forms** & High-Performance Embedded SQLite Full-Text Search.

---

## 🎯 Problem It Solves
Office 365 Outlook default search often experiences severe lag, timeouts, and latency because every keystroke queries Microsoft cloud Exchange servers online.

**BettyMailZoom** indexes your Outlook mailboxes (Inbox, Sent Items, Archive, Custom Folders) into a fast, local embedded SQLite database on your machine. Queries run in **less than 10 milliseconds** completely offline!

---

## ✨ Features

- **⚡ Sub-Second Local Search**: Full-text searching across Subject, Sender, Recipients, Body content, and Attachment names with sub-10ms response times.
- **🔍 Multi-Criteria Filter Bar**:
  - **From / Sender**: Target specific senders or email domains.
  - **To / Recipient**: Search by `To:` and `Cc:` recipients.
  - **Subject**: Target specific email titles.
  - **Exclude Terms**: Filter out unwanted noise (e.g. `newsletter`, `spam`, `digest`, `automated`).
  - **Attachments**: Filter by Has Attachment / No Attachment + file extension (e.g. `.pdf`, `.xlsx`, `.docx`, `.zip`).
  - **Importance**: Filter by High Priority (🔴), Normal, or Low Priority (🔵).
  - **Date Range**: Quick presets (Today, Past 7 Days, Past 30 Days, Past 6 Months, This Year, Custom Date Range).
  - **Folders**: Filter by specific mailbox or folder.
  - **Unread Only**: Toggle unread emails.
- **👁 Live Preview Pane**:
  - Split view (Right / Bottom / Hidden).
  - Formatted HTML view with Plain Text toggle.
  - Interactive attachment list with file chips (Click to save or open).
- **📧 Native Outlook Actions**:
  - **Open in Outlook**: 1-click or double-click to view the email directly in the native Outlook window.
  - **Delete Email**: Soft delete / move to trash in Outlook and immediately update local search index.
  - **Rich Clipboard Copy**: Copy Subject, Copy Sender, Copy Body, or Copy All Email Information (`Ctrl+C`).
- **🔄 Sync & Rebuild Management**:
  - **Refresh Index (Delta Sync)**: Quick sync that only scans for new/modified emails since the last sync time.
  - **Rebuild Index**: Complete clean rebuild from scratch.
  - **Folder Selection**: Choose which Outlook accounts and folders to index.
  - **Background Auto-Sync**: Configurable automatic background sync interval (5, 15, 30, or 60 minutes).

---

## ⌨ Keyboard Shortcuts

| Shortcut | Action |
|---|---|
| `Enter` / Double-click | Open selected email in Microsoft Outlook |
| `Ctrl + C` | Copy full email details to Clipboard |
| `Delete` | Delete selected email |
| `Ctrl + F` | Focus search bar |
| `F5` / `Enter` (in search) | Run search query |

---

## 🚀 How to Run

### Binary Location
The compiled release executable is located at:
```
BettyMailZoom\bin\Release\net48\BettyMailZoom.exe
```

### Run via Command Line
```powershell
.\bin\Release\net48\BettyMailZoom.exe
```

### Run Built-in Automated Tests & Benchmark
```powershell
.\bin\Release\net48\BettyMailZoom.exe --test
```
