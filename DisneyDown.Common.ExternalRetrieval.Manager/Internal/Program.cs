using DisneyDown.Common.ExternalRetrieval.Manager.UI;
using System;
using System.Windows.Forms;

namespace DisneyDown.Common.ExternalRetrieval.Manager.Internal
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home());
        }
    }
}