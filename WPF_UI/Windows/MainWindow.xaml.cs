using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MMMELibs;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Utilities utilities; EncoderUtilities encode_util = null;
        ConfigUtilities config;
        public static Reference reference = null;
        bool HasAborted = false;
        MMME.Windows.Settings settingsWindow = null;

        public MainWindow()
        {
            InitializeComponent();
            onStartUp();
        }

        private void onStartUp()
        {
            reference = new Reference();
            utilities = reference.GetUtilities;
            config = reference.GetConfig;
            reference.ConfigUtil = config;
            reference.setLogBlock(ConsoleOutputTxtBlk);
            reference.setScrollView(ConsoleOutputScrView);
            if (config.LastUsedMediaDirectory != "")
            {
                FileLocationTxb.Text = config.LastUsedMediaDirectory;
            }
            reference.OriginalSize = OriginalSize_Txb;
            reference.CurrentSize = CurrentSize_Txb;
            encode_util = new EncoderUtilities(reference);
            string startTime = DateTime.Now.ToString();
            //settingsWindow = new MMME.Windows.Settings(reference);
            reference.Log.Add("------------Log Start-------------------");

        }


        private void OpenFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            string dir = string.Empty;
            if (FileLocationTxb.Text != string.Empty || FileLocationTxb.Text != null)
            {
                try
                {
                    dir = FileUtilities.OpenFolder(FileLocationTxb.Text, "Select the root folder of the media files.");

                }
                catch (Exception)
                {
                    dir = FileUtilities.OpenFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Select the root folder of the media files.");
                }
            }
            else
            {
                dir = FileUtilities.OpenFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Select the root folder of the media files.");
            }

            if (dir != string.Empty && dir != "" && dir != " ")
            {
                FileLocationTxb.Text = dir;
                reference.Log.Add(@"File location set to " + dir);
                config.LastUsedMediaDirectory = dir;
            }
            else
            {
                FileLocationTxb.Text = "Source ";
                reference.Log.Add(@"File location was not found");
            }
        }

        private void Report()
        {
            string time = DateTime.Now.ToString();
            string text = "";
            foreach (string s in reference.Log)
            {
                text += time + ": " + s + "\n";
            }
            ConsoleOutputTxtBlk.Text = text;
            ConsoleOutputScrView.ScrollToBottom();
        }

        private async Task<string> Update()
        {
            var value = await Task.Run(() =>
            {
                var now = DateTime.Now;
                while (true)
                {
                    string time = DateTime.Now.ToString();
                    string text = "";
                    foreach (string s in reference.Log)
                    {
                        text += time + ": " + s + "\n";
                    }
                    return text;
                }
            });
            ConsoleOutputScrView.ScrollToBottom();
            ConsoleOutputTxtBlk.Text = value;
            return value;
        }

        private void Clear(object value)
        {
            if (value.GetType().Equals(typeof(TextBlock)))
            {
                var item = (TextBlock)value;
                item.Text = string.Empty;
            }
            else if (value.GetType().Equals(typeof(TextBox)))
            {
                var item = (TextBox)value;
                item.Text = string.Empty;
            }
        }

        private async void ProcessBtn_Click(object sender, RoutedEventArgs e)
        {
            if (config.IsNetworkPath && config.UseEnclosedFolder)
            {
                reference.Log.Add("No Temp Directory Specified", "A Temp Directory needs to be specified if the file is on a network path");
                return;
            }

            if (!config.UseEnclosedFolder && config.TempFolderDirectory.Equals(""))
            {
                reference.Log.Add("No Temp Directory Specified");
                return;
            }
            if (FileLocationTxb.Text.ToLower() == "source")
            {
                reference.Log.Add("No Directory Specified");
                return;
            }

            var file = await FileUtilities.GetFilesAsync(FileLocationTxb.Text);

            if (file.Count() == 0)
            {
                reference.Log.Add("No Media Files Found in " + FileUtilities.directory);
                return;
            }
            var button = MessageBoxButton.YesNo;
            var notify = MessageBox.Show("This could take a REALLY long time! It may seem frozen, but its not.\nWould you like to continue?", "Warning", button);
            if (notify == MessageBoxResult.No)
            {
                reference.Log.Add("Process Canceled");
                reference.Log.Add("No Hard Feelings.");
                return;
            }
            ProcessBtn.IsEnabled = false;
            foreach (string value in file)
            {
                if (HasAborted) break;
                await Task.Run(() => encode_util.ProcessFileAsync(value, ConsoleOutputTxtBlk, ProcessBtn));
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (encode_util == null || encode_util.process == null || encode_util.process.HasExited)
            {
                Close();
                return;
            }
            var result = MessageBox.Show("Are you sure you want to quit?\nThis Could corrupt any unprocessed files.", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
            else
            {
                reference.Log.Add("Exit Canceld!");
                reference.Log.Add("Encoding Still Running!");
            }
        }

        public new void Close()
        {
            encode_util.Abort(false);
            string endTime = DateTime.Now.ToString();
            reference.Log.Add("--------------Log End---------------------" + Environment.NewLine);
            reference.Log.Close();
            base.Close();
            Environment.Exit(0);
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((encode_util != null && encode_util.process != null && !encode_util.process.HasExited) || !encode_util.ready)
            {
                var result = MessageBox.Show("Are you sure you want to Abort?\nThis Could corrupt any unprocessed files.", "Warning", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    encode_util.Abort(false);
                    HasAborted = true;
                }
            }
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (settingsWindow == null)
                settingsWindow = new MMME.Windows.Settings(reference);

            settingsWindow.Show();
            if (!settingsWindow.IsLoaded)
                settingsWindow.Show();
            else if (settingsWindow.WindowState == WindowState.Minimized)
                settingsWindow.WindowState = WindowState.Normal;
            else
                settingsWindow.Activate();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SkipBtn_Click(object sender, RoutedEventArgs e)
        {
            if (encode_util != null && encode_util.process != null && !encode_util.process.HasExited)
            {
                encode_util.Abort(false);
            }
        }
    }
}
