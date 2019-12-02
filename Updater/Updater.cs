using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;

namespace ChaseLabs.Updater
{
    public class Updater
    {
        private ISharpUpdateable applicationInfo;
        private BackgroundWorker bgWorker;


        public Updater(ISharpUpdateable applicationInfo)
        {
            this.applicationInfo = applicationInfo;

            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
        }

        public void DoUpdate()
        {
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync(applicationInfo);
        }


        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ISharpUpdateable app = (ISharpUpdateable)e.Argument;

            if (!UpdateXml.ExistsOnServer(app.UpdateXmlLocation))
                e.Cancel = true;
            else
                e.Result = UpdateXml.Parse(app.UpdateXmlLocation, app.ApplicationID);
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                UpdateXml update = (UpdateXml)e.Result;
                if (update != null && update.IsNewerThan(applicationInfo.ApplicationAssembly.GetName().Version))
                {
                    if (new SharpUpdateAcceptForm(applicationInfo, update).ShowDialog(new WindowWrapper(new WindowInteropHelper(applicationInfo.Context).Handle)) == DialogResult.Yes)
                    {
                        DownloadUpdate(update);
                    }
                }
            }
        }

        private void DownloadUpdate(UpdateXml update)
        {
            SharpUpdateDownloadForm form = new SharpUpdateDownloadForm(update.Uri, update.Md5, null);
            DialogResult result = form.ShowDialog(new WindowWrapper(new WindowInteropHelper(applicationInfo.Context).Handle));
            if (result == DialogResult.OK)
            {
                string currentPath = applicationInfo.ApplicationAssembly.Location;
                string startFileName = Path.Combine(Path.GetDirectoryName(currentPath), update.FileName);

                UpdateApplication(form.tempFilePath, currentPath, startFileName, update.LaunchArgs);
                Application.Exit();
            }
            else if (result == DialogResult.Abort)
            {
                MessageBox.Show("The Update Download was Cancelled.\nThis Program has not been modified", "Update Download Cancelled.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("There was an Unexpected Error while downloading this update\nPlease Try Again Later...", "Update Download Error.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateApplication(string tempFilePath, string currentPath, string startFileName, string launchArgs)
        {
            var files = UnZipApplication(tempFilePath);
            //string argument = $"/C Choice /C Y /N /D Y /T 4 & Del /F /Q \"{currentPath}\" & Choice /C Y /N /D Y /T 2 & Move /Y \"{tempFilePath}\" \"{newPath}\" & Start \"\" /D \"{Path.GetDirectoryName(newPath)}\" \"{Path.GetFileName(newPath)}\" {launchArgs}";
            //ProcessStartInfo info = new ProcessStartInfo();
            //info.Arguments = argument;
            //info.WindowStyle = ProcessWindowStyle.Hidden;
            //info.CreateNoWindow = true;
            //info.FileName = "cmd.exe";
            //Process.Start(info);

            var moveTask = Task.Run(new Action(() =>
            {
                foreach (var file in files)
                {
                    File.Move(file, currentPath);
                }
            }));

            moveTask.Wait();
            if (moveTask.IsFaulted || moveTask.IsCanceled)
                return;
            Directory.Delete(tempFilePath);

            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = $"\"{Path.Combine(Environment.CurrentDirectory, AppDomain.CurrentDomain.FriendlyName)}\""
            };

            Process.Start(info);

            Environment.Exit(0);

        }

        private List<string> UnZipApplication(string tempFilePath)
        {
            try
            {
                ZipFile.ExtractToDirectory(tempFilePath, Directory.GetParent(tempFilePath).FullName);
                return (List<string>)Directory.GetFiles(Directory.GetParent(tempFilePath).FullName).GetEnumerator();

            }
            catch
            {

            }
            return new List<string>();
        }
    }
}
