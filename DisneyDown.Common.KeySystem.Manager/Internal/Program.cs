using DisneyDown.Common.KeySystem.Manager.UI;
using System;
using System.Windows.Forms;
using UIHelpers;

namespace DisneyDown.Common.KeySystem.Manager.Internal
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //attempt to load the connection file
            var connection = Connection.FromConnectionFile();

            //validation
            if (connection != null)
            {
                //set global
                Common.Globals.KeyServerConnection = connection;

                //setup the application
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Home());
            }
            else
            {
                //alert the user
                UIMessages.Error(@"Couldn't load the key server configuration file");
            }
        }
    }
}