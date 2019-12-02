using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaseLabs.Updater
{
    public partial class SharpUpdateInfoForm : Form
    {
        internal SharpUpdateInfoForm(ISharpUpdateable applicationInfo, UpdateXml updateInfo)
        {
            InitializeComponent();

            //if (applicationInfo.ApplicationIcon != null)
            //    Icon = applicationInfo.ApplicationIcon;
            Text = applicationInfo.ApplicationName + " - Update Info";
            lblVersion.Text = string.Format("Current Version: {0}\nUpdate Version: {1}", applicationInfo.ApplicationAssembly.GetName().Version.ToString(), updateInfo.Version);
            txtDescription.Text = updateInfo.Changelog;
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Control && e.KeyCode == Keys.C))
                e.SuppressKeyPress = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
