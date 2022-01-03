using System;
using System.Windows.Forms;
using UIHelpers;

namespace DisneyDown.Common.ExternalRetrieval.Manager.UI
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                //report error
                UIMessages.Error($"Error occurred whilst trying to load dependency information:\n\n{ex}");

                //kill app
                Close();
            }
        }
    }
}