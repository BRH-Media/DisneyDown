using System;
using System.Windows.Forms;

namespace DisneyDown.Common.SettingsConfigurator.UI
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void LstSettingsSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            //convert the selected item to a string
            var settingsSection = lstSettingsSections.SelectedItem.ToString();

            //what do we do?
            switch (settingsSection)
            {

            }
        }

        private void ItmApplySettings_Click(object sender, EventArgs e)
        {
        }

        private void ItmRevertSettings_Click(object sender, EventArgs e)
        {
        }

        private void Home_Load(object sender, EventArgs e)
        {
            //first settings profile is always selected by default
            if (lstSettingsSections.Items.Count > 0)
            {
                //this is done to ensure that the user always sees loaded settings
                //when it is loaded up; otherwise, it's an unnecessary extra step for
                //the user.
                lstSettingsSections.SelectedIndex = 0;
            }
        }
    }
}