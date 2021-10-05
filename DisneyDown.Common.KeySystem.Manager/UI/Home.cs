using DisneyDown.Common.Extensions;
using DisneyDown.Common.Util.Kit.WaitWIndow;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UIHelpers;

// ReSharper disable InvertIf

namespace DisneyDown.Common.KeySystem.Manager.UI
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void ItmHelpAbout_Click(object sender, System.EventArgs e)

            //show about dialog
            => new About().ShowDialog();

        private void Home_Load(object sender, System.EventArgs e)
        {
            //begin user authentication
            SetupUser();

            //begin key query
            SetupKeys();
        }

        private void SetupUserLabel()
        {
            if (InvokeRequired)

                BeginInvoke((MethodInvoker)SetupUserLabel);
            else
            {
                //setup UI
                lblUserValue.ForeColor = Color.Black;
                lblUserValue.Text = $@"{Common.Globals.KeyServerUser.FirstName} {Common.Globals.KeyServerUser.LastName}" +
                                    (Common.Globals.KeyServerUser.IsAdmin ? " (Administrator)" : " (Standard)");
            }
        }

        private void SetupData(DataTable data)
        {
            if (InvokeRequired)

                BeginInvoke((MethodInvoker)delegate
                {
                    SetupData(data);
                });
            else
            {
                //apply the result
                dgvMain.DataSource = data;

                //apply viewing label
                lblViewingValue.Text = $@"{data.Rows.Count}/{data.Rows.Count}";
            }
        }

        private void CrossThreadedClose()
        {
            if (InvokeRequired)

                BeginInvoke((MethodInvoker)Close);
            else

                Close();
        }

        private void SetupKeysCallback(object sender, WaitWindowEventArgs e)
            => SetupKeys(false);

        private void SetupKeys(bool waitWindow = true)
        {
            //multi-threaded operation
            if (waitWindow)
            {
                //show wait window GUI to avoid lock-ups
                WaitWindow.Show(SetupKeysCallback, @"Querying...");
            }
            else
            {
                //call for the keys
                var keys = Common.Globals.KeyServerConnection.GetAuthorisedKeys(Common.Globals.KeyServerUser);

                //validation
                if (keys != null)
                {
                    //success validation
                    if (keys.Status.Success)
                    {
                        //length validation
                        if (keys.Data.Length > 0)
                        {
                            //convert to DataTable
                            var data = keys.Data.ToDataTable();

                            //validation
                            if (data != null)
                            {
                                //setup UI
                                SetupData(data);
                            }
                        }
                    }
                    else
                    {
                        //display error to user
                        UIMessages.Error($"Query error: {keys.Status.Message} ({keys.Status.Code})");

                        //kill form
                        CrossThreadedClose();
                    }
                }
                else
                {
                    //alert user
                    UIMessages.Error(@"Query failed: key system object was null");

                    //kill form
                    CrossThreadedClose();
                }
            }
        }

        private void SetupUserCallback(object sender, WaitWindowEventArgs e)
            => SetupUser(false);

        private void SetupUser(bool waitWindow = true)
        {
            //multi-threaded operation
            if (waitWindow)
            {
                //show wait window GUI to avoid lock-ups
                WaitWindow.Show(SetupUserCallback, @"Authenticating...");
            }
            else
            {
                //call for the user
                var user = Common.Globals.KeyServerConnection.GetCurrentUser();

                //validation
                if (user != null)
                {
                    //success validation
                    if (user.Status.Success)
                    {
                        //apply global
                        Common.Globals.KeyServerUser = user.Data.UserInformation;

                        //setup UI
                        SetupUserLabel();

                        //welcome user
                        UIMessages.Info($"Welcome {Common.Globals.KeyServerUser.FirstName}!");
                    }
                    else
                    {
                        //display error to user
                        UIMessages.Error($"Authentication error: {user.Status.Message} ({user.Status.Code})");

                        //kill form
                        CrossThreadedClose();
                    }
                }
                else
                {
                    //alert user
                    UIMessages.Error(@"Authentication failed: user object was null");

                    //kill form
                    CrossThreadedClose();
                }
            }
        }

        private void ItmDataWhitelist_Click(object sender, System.EventArgs e)
            => new Whitelist().ShowDialog();
    }
}