using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using ChaseLabs.Updater;
using MMMELibs;

namespace ChaseLabs.Updater
{
    internal partial class SharpUpdateDownloadForm : Form
    {

        internal WebClient webClient;
        internal BackgroundWorker bgWorker;
        internal string tempFile;
        internal string md5;

        internal string tempFilePath
        {
            get
            {
                return tempFile;
            }
        }


        internal SharpUpdateDownloadForm(Uri location, string md5, Icon programIcon)
        {
            InitializeComponent();

            if (programIcon != null)
                Icon = programIcon;

            tempFile = Path.GetTempFileName();
            this.md5 = md5;

            webClient = new WebClient();
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;

            try { webClient.DownloadFileAsync(location, tempFile); } catch { DialogResult = DialogResult.No; }
        }

        internal void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblProgress.Text = string.Format("Downloaded {0} of {1}", FileUtilities.AdjustedFileSize(e.BytesReceived), FileUtilities.AdjustedFileSize(e.TotalBytesToReceive));
        }

        internal void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                DialogResult = DialogResult.No;
            }
            else if (e.Cancelled)
            {
                DialogResult = DialogResult.Abort;
                Close();
            }
            else
            {
                lblProgress.Text = "Verifying Download...";
                progressBar.Style = ProgressBarStyle.Marquee;

                bgWorker.RunWorkerAsync(new string[] { tempFile, md5 });
            }
        }


        internal void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string file = ((string[])e.Argument)[0];
            string updateMd5 = ((string[])e.Argument)[1];

            if (Hasher.HashFile(file, HashType.MD5) != updateMd5)
                e.Result = DialogResult.No;
            else
                e.Result = DialogResult.OK;
        }
        internal void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DialogResult = (DialogResult)e.Result;
            Close();
        }

        internal void SharpUpdateDownloadForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (webClient.IsBusy)
            {
                webClient.CancelAsync();
                DialogResult = DialogResult.Abort;
            }

            if (bgWorker.IsBusy)
            {
                bgWorker.CancelAsync();
                DialogResult = DialogResult.Abort;
            }
        }
    }
}
