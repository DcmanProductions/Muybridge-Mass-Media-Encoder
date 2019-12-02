using System;
using System.Windows.Forms;

namespace ChaseLabs.Updater
{
    internal partial class SharpUpdateAcceptForm : Form
    {

        private ISharpUpdateable applicationInfo;
        private UpdateXml updateInfo;
        private SharpUpdateInfoForm updateInfoForm;

        internal SharpUpdateAcceptForm(ISharpUpdateable applicationInfo, UpdateXml updateInfo)
        {
            InitializeComponent();
            this.applicationInfo = applicationInfo;
            this.updateInfo = updateInfo;
            Text = this.applicationInfo.ApplicationName + " - Update Available";

            //if (this.applicationInfo.ApplicationIcon != null)
            //    Icon = applicationInfo.ApplicationIcon;
            lblNewVersion.Text = string.Format("New Version {0}", this.updateInfo.Version.ToString());
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (updateInfoForm == null)
            {
                updateInfoForm = new SharpUpdateInfoForm(applicationInfo, updateInfo);
                updateInfoForm.ShowDialog(this);
            }
        }
    }
}
