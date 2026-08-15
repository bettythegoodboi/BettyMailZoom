using System;
using System.Windows.Forms;
using BettyMailZoom.Forms;

namespace BettyMailZoom
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if (args != null && args.Length > 0 && args[0] == "--test")
            {
                BettyMailZoom.Tests.TestRunner.RunTests();
                return;
            }

            // Global error handlers
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += (sender, a) =>
            {
                if (a.ExceptionObject is Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "BettyMailZoom Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
        }
    }
}
