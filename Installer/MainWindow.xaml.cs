using System;
using System.IO;
using System.Windows;

namespace Installer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string location = "";
        public MainWindow()
        {
            InitializeComponent();
            InstallLocationTxbl.Text = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}/ChaseLabs/Muybridge Mass Media Encoder/";
            location = InstallLocationTxbl.Text;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are You Sure You want to Exit the Installer?", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                Close();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }

        private void InstalllocationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!OpenFolder($@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}").Equals(""))
            {

            }
        }

        string OpenFolder(params string[] args)
        {
            using (var folder = new System.Windows.Forms.FolderBrowserDialog())
            {
                folder.ShowNewFolderButton = false;
                folder.Description = "Select the Install Directory";
                if (args.Length != 0)
                {
                    folder.SelectedPath = args[0];
                }
                System.Windows.Forms.DialogResult result = folder.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folder.SelectedPath))
                {
                    string[] files = Directory.GetFiles(folder.SelectedPath);

                    return folder.SelectedPath;
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    return string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private void InstallBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!location.Equals(string.Empty))
            {
                var install = new WSVMLib.Github.Installer();
                if (install.InstallFromGitHub("dcmanproductions", "Muybridge-Mass-Media-Encoder", location, $@"{Environment.GetEnvironmentVariable("appdata")}/Muybridge/MassMediaEncoder/Update/Temp/", "Muybridge Mass Media Encoder.exe"))
                {
                    MessageBox.Show("Installation Successful");
                }
                else
                {
                    MessageBox.Show("Installation Failed");
                }
            }
        }
    }
}
