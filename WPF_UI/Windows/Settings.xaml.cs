using MMMELibs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChaseLabs.Updater;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Windows.Media;

namespace MMME.Windows
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window, ISharpUpdateable
    {
        Utilities utilities;
        ConfigUtilities configUtilities;
        Reference reference;
        List<Button> navButtons;

        public Settings(Reference _ref)
        {
            InitializeComponent();
            reference = _ref;
            utilities = reference.GetUtilities;
            configUtilities = reference.GetConfig;
            onStartUp();
        }

        private void onStartUp()
        {
            reference.ConfigUtil = configUtilities;
            LogLocation_TxtBx.Text = reference.LogLocation;
            ConfigLocation_TxtBx.Text = reference.ConfigLocation;
            RootLocation_TxtBx.Text = reference.RootLocation;
            InstallationLocation_TxtBx.Text = reference.InstallationFolder;
            Overwrite_CKBX.IsChecked = configUtilities.OverWriteOriginal;
            reference.BuildVersion = ApplicationAssembly.GetName().Version.ToString();
            build_txtblk.Text = reference.BuildVersion;

            EnclosedFolderOpenBtn.IsEnabled = !configUtilities.UseEnclosedFolder;
            TempEncodeFolder_CKBX.IsChecked = configUtilities.UseEnclosedFolder;
            EnclosedFolderTxtBox.Text = configUtilities.TempFolderDirectory;

            EncoderConsole_CKBX.IsChecked = configUtilities.ShowEncodeConsole;
            IsNetworkPath_CKBX.IsChecked = configUtilities.IsNetworkPath;

            navButtons = new List<Button>();
            addNavItems();

            foreach (var item in Enum.GetValues(typeof(SortType)))
            {
                sortType_comboBox.Items.Add(item);
            }
            sortType_comboBox.Foreground = System.Windows.Media.Brushes.Black;

        }

        private void addNavItems()
        {
            foreach (TabItem item in contentTabController.Items)
            {
                Button btn = new Button
                {
                    Name = item.Header.ToString() + "Btn",
                    Content = item.Header.ToString(),
                    Style = Application.Current.Resources["NavButton"] as Style
                };

                btn.Click += nav_button_Clicked;

                navButtons.Add(btn);
                NavigationBar.Children.Add(btn);

                TextBlock tblk = new TextBlock
                {
                    Text = item.Header.ToString(),
                    Name = item.Header.ToString() + "TxtBx",
                    FontSize = 72,
                    Margin = new Thickness
                    {
                        Top = 48,
                        Left = 31,
                        Bottom = 0,
                        Right = 0
                    },
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 72 * item.Header.ToString().Length + 15,
                    Height = 72 + 15,
                };
                Grid grid = item.Content as Grid;
                grid.Children.Add(tblk);
                Grid.SetColumn(tblk, 1);
            }
        }

        private void nav_button_Clicked(object sender, RoutedEventArgs e)
        {
            if (sender.GetType().Equals(typeof(Button)))
            {
                Button btn = (Button)sender;
                foreach (TabItem tab in contentTabController.Items)
                {
                    if (btn.Content.Equals(tab.Header.ToString()))
                    {
                        contentTabController.SelectedItem = tab;
                    }
                }

            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OpenConfigLocation_Click(object sender, RoutedEventArgs e)
        {
            utilities.OpenFolder(reference.ConfigLocation);
        }

        private void OpenLogLocation_Click(object sender, RoutedEventArgs e)
        {
            utilities.OpenFolder(reference.LogLocation);
        }

        private void OpenRootLocation_Click(object sender, RoutedEventArgs e)
        {
            utilities.OpenFolder(reference.RootLocation);
        }

        private void Overwrite_CKBX_Checked(object sender, RoutedEventArgs e)
        {
            configUtilities.OverWriteOriginal = (bool)Overwrite_CKBX.IsChecked;
        }

        private void OpenLogFile_Click(object sender, RoutedEventArgs e)
        {
            utilities.OpenFile(System.IO.Path.Combine(reference.LogLocation, "latest.log"));
        }

        private void OpenConfigFile_Click(object sender, RoutedEventArgs e)
        {
            utilities.OpenFile(System.IO.Path.Combine(reference.ConfigLocation, configUtilities.default_config_file));
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            configUtilities.Export();
            reference.Log.Add("Saved!");
        }

        private void Clean_Logs_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(reference.LogFileLocation))
                Directory.Delete(Directory.GetParent(reference.LogFileLocation).FullName, true);
        }

        private void OpenInstallationLocation_Click(object sender, RoutedEventArgs e)
        {
            utilities.OpenFolder(reference.InstallationFolder);
        }

        private void UninstallBtn_Click(object sender, RoutedEventArgs e)
        {
            Uninstall();
        }

        private async Task Uninstall()
        {

        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private async Task Update()
        {
        }


        public string InstallationExecFile
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            }
        }

        private void EvolveTabBtn_Click(object sender, RoutedEventArgs e)
        {
            if (contentTabController.SelectedIndex == contentTabController.Items.Count)
            {
                contentTabController.SelectedIndex = 0;
            }
            else
            {
                contentTabController.SelectedIndex++;
            }

            //TabTextTb.Text = (contentTabController.SelectedItem as TabItem).Header.ToString();
        }

        private void DevolveTabBtn_Click(object sender, RoutedEventArgs e)
        {
            if (contentTabController.SelectedIndex == 0)
            {
                contentTabController.SelectedIndex = contentTabController.Items.Count;
            }
            else
            {
                contentTabController.SelectedIndex--;
            }

            //TabTextTb.Text = (contentTabController.SelectedItem as TabItem).Header.ToString();
        }

        private void TempEncodeFolder_CKBX_Click(object sender, RoutedEventArgs e)
        {
            configUtilities.UseEnclosedFolder = (bool)TempEncodeFolder_CKBX.IsChecked;
            EnclosedFolderOpenBtn.IsEnabled = !(bool)TempEncodeFolder_CKBX.IsChecked;
        }

        private void EnclosedFolderOpenBtn_Click(object sender, RoutedEventArgs e)
        {
            reference.GetUtilities.OpenFolder(configUtilities.TempFolderDirectory);

            string dir = string.Empty;
            if (EnclosedFolderTxtBox.Text != string.Empty || EnclosedFolderTxtBox.Text != null)
            {
                try
                {
                    dir = FileUtilities.OpenFolder(EnclosedFolderTxtBox.Text, "Select the temp folder for the media files.");

                }
                catch (Exception)
                {
                    dir = FileUtilities.OpenFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Select the temp folder for the media files.");
                }
            }
            else
            {
                dir = FileUtilities.OpenFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Select the temp folder for the media files.");
            }

            if (dir != string.Empty && dir != "" && dir != " ")
            {
                EnclosedFolderTxtBox.Text = dir;
                reference.Log.Add(@"File location set to " + dir);
                configUtilities.TempFolderDirectory = dir;
            }
            else
            {
                EnclosedFolderTxtBox.Text = "Source ";
                reference.Log.Add(@"File location was not found");
            }
        }

        private void EncoderConsole_CKBX_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType().Equals(typeof(CheckBox)))
            {
                CheckBox check = (CheckBox)sender;
                reference.ConfigUtil.ShowEncodeConsole = (bool)check.IsChecked;
            }
        }

        private void IsNetworkPath_CKBX_Click(object sender, RoutedEventArgs e)
        {
            configUtilities.IsNetworkPath = (bool)((CheckBox)sender).IsChecked;
        }



        private void sortType_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public string ApplicationName => "Muybridge Mass Media Encoder";

        public string ApplicationID => "MME";

        public Assembly ApplicationAssembly => Assembly.GetExecutingAssembly();

        public Uri UpdateXmlLocation => new Uri("");

        public Window Context => this;

        public ImageSource ApplicationIcon => Icon;

    }
}
