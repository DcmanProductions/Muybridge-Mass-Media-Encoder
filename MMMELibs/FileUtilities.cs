using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;

namespace MMMELibs
{
    public class FileUtilities
    {
        static Dispatcher dis = Dispatcher.CurrentDispatcher;

        public static string[] MediaExtensions
        {
            get
            {
                return new string[] { "done", "mp4", "mkv", "m4a", "m4v", "f4v", "f4a", "m4b", "m4r", "f4b", "mov", "3gp", "3gp2", "3g2", "3gpp", "3gpp2", "ogg", "oga", "ogv", "ogx", "wmv", "wma", "flv", "avi" };
            }
        }

        public static string directory;

        public static string OpenFolder(params string[] args)
        {

            using (var folder = new FolderBrowserDialog())
            {
                folder.ShowNewFolderButton = false;
                if (args.Length != 0)
                {
                    folder.SelectedPath = args[0];
                }
                if (args.Length >= 2)
                {
                    folder.Description = args[1];
                }
                DialogResult result = folder.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folder.SelectedPath))
                {
                    return folder.SelectedPath;
                }
                else if (result == DialogResult.Cancel)
                {
                    return string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static async Task<List<string>> GetFilesAsync(params string[] _dir)
        {
            string dir = string.Empty;
            if (_dir.Length == 0)
                dir = OpenFolder();
            else
                dir = _dir[0];
            if (dir != null || dir != string.Empty)
            {
                directory = dir;
                var files_task = await Task.Run(() => Files(dir));
                return files_task;
            }
            return null;
        }

        private static List<string> Files(string dir)
        {
            dis.Invoke(new Action(() =>
            {
                Utilities.reference.Log.Add("Processing Directories and all Subdirectories.");
            }));
            List<string> l = new List<string>();
            foreach (string s in MediaExtensions)
            {
                foreach (string j in Directory.GetFiles(dir, "*." + s, SearchOption.AllDirectories))
                {

                    FileInfo fileInfo = new FileInfo(j);
                    if (!fileInfo.Name.Contains("done"))
                    {
                        dis.Invoke(new Action(() =>
                        {
                            Utilities.reference.Log.Add($"Found {fileInfo.Name}");
                        }));
                    }
                    bool should_add = true;
                    foreach (var f in Directory.GetParent(j).GetFiles())
                    {
                        if (f.Name.Contains(".done"))
                            should_add = false;
                    }
                    if (should_add)
                    {
                        dis.Invoke(new Action(() =>
                        {
                            var MediaFiles = Utilities.reference.MediaFiles;
                            MediaFiles.Add(new MediaFile() { FilePath = j, Size = new FileInfo(j).Length, ID = Utilities.reference.MediaFiles.Count + 1 });
                            //MediaFiles.GetFiles(MediaFiles.);
                        }));

                        l.Add(j);
                    }
                }
            }
            return l;
        }

        public static long FileSizeBytes(string file)
        {
            FileInfo info = new FileInfo(file);
            long num = info.Length;
            return num;
        }
        public static double FileSizeKB(string file)
        {
            double num = FileSizeBytes(file) / 1024;
            double.TryParse("" + num, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            num = Math.Round(num, 2);
            return num;
        }
        public static double FileSizeMB(string file)
        {
            double num = FileSizeKB(file) / 1024;
            double.TryParse("" + num, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            num = Math.Round(num, 2);
            return num;
        }
        public static double FileSizeGB(string file)
        {
            double num = FileSizeMB(file) / 1024;
            double.TryParse("" + num, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            num = Math.Round(num, 2);
            return num;
        }


        public static double FileSizeKB(double file)
        {
            double num = file / 1024;
            double.TryParse("" + num, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            num = Math.Round(num, 2);
            return num;
        }
        public static double FileSizeMB(double file)
        {
            double num = FileSizeKB(file) / 1024;
            double.TryParse("" + num, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            num = Math.Round(num, 2);
            return num;
        }
        public static double FileSizeGB(double file)
        {
            double num = FileSizeMB(file) / 1024;
            double.TryParse("" + num, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            num = Math.Round(num, 2);
            return num;
        }

        public static string AdjustedFileSize(string file)
        {
            return (FileSizeBytes(file) < 1024) ? FileSizeBytes(file) + "B" : (FileSizeKB(file) < 1024) ? FileSizeKB(file) + "KB" : (FileSizeMB(file) < 1024) ? FileSizeMB(file) + "MB" : FileSizeGB(file) + "GB";
        }

        public static string AdjustedFileSize(double size)
        {
            return (Math.Round(double.Parse("" + size, NumberStyles.Any, CultureInfo.InvariantCulture), 2) < 1024) ? Math.Round(double.Parse("" + size, NumberStyles.Any, CultureInfo.InvariantCulture), 2) + "B" : (FileSizeKB(size) < 1024) ? FileSizeKB(size) + "KB" : (FileSizeMB(size) < 1024) ? FileSizeMB(size) + "MB" : FileSizeGB(size) + "GB";
        }



    }
}
