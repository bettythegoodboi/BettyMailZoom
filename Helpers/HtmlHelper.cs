using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BettyMailZoom.Helpers
{
    public static class HtmlHelper
    {
        public static string WrapEmailHtml(string htmlBody, string plainTextFallback)
        {
            if (string.IsNullOrWhiteSpace(htmlBody))
            {
                if (string.IsNullOrWhiteSpace(plainTextFallback))
                {
                    return "<html><body style='font-family:Segoe UI, sans-serif; color:#666; padding:20px; font-size:13px;'><i>(No message body content)</i></body></html>";
                }
                
                string encoded = HttpUtility.HtmlEncode(plainTextFallback)
                    .Replace("\r\n", "<br/>")
                    .Replace("\n", "<br/>");
                
                return $@"<!DOCTYPE html>
<html>
<head>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
    <meta charset='utf-8' />
    <style>
        body {{
            font-family: 'Segoe UI', Arial, sans-serif;
            font-size: 13px;
            color: #222;
            background-color: #ffffff;
            margin: 15px;
            line-height: 1.5;
            word-wrap: break-word;
        }}
    </style>
</head>
<body>
    {encoded}
</body>
</html>";
            }

            // Inject modern IE compatibility and base styles if missing
            string styledHtml = htmlBody;
            
            if (styledHtml.IndexOf("X-UA-Compatible", StringComparison.OrdinalIgnoreCase) < 0)
            {
                styledHtml = "<meta http-equiv='X-UA-Compatible' content='IE=edge' />" + styledHtml;
            }

            // Add standard CSS reset to prevent weird overflow
            string customCss = @"
                <style>
                    body { font-family: 'Segoe UI', -apple-system, BlinkMacSystemFont, Roboto, Helvetica, Arial, sans-serif !important; word-wrap: break-word; }
                    img { max-width: 100% !important; height: auto !important; }
                    table { max-width: 100% !important; }
                </style>
            ";

            if (styledHtml.IndexOf("<head>", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var regex = new Regex("<head>", RegexOptions.IgnoreCase);
                styledHtml = regex.Replace(styledHtml, "<head>" + customCss, 1);
            }
            else
            {
                styledHtml = customCss + styledHtml;
            }

            return styledHtml;
        }
    }
}
