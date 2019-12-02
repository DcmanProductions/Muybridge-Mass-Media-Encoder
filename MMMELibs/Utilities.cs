using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MMMELibs
{
    public class Utilities
    {
        public static Reference reference;
        public Utilities(Reference _ref)
        {
            reference = _ref;
        }


        public void OpenFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }
            else
            {
                reference.Log.Add(string.Format("{0} Directory does not exist!", folderPath));
            }
        }

        public void OpenFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    //Arguments = filePath,
                    FileName = filePath//"explorer.exe"
                };

                Process.Start(startInfo);
            }
            else
            {
                reference.Log.Add(string.Format("{0} File does not exist!", filePath));
            }
        }
    }
}
