using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MMMELibs
{

    public class EncoderUtilities
    {
        Utilities util;
        Dispatcher dis = Dispatcher.CurrentDispatcher;
        string currentDirectory;
        public Process process;
        public bool HasAborted;
        private Button processBtn;
        string new_file, file;
        string original_path;
        Reference reference;
        public bool should_run = true, ready = false;
        public EncoderUtilities(Reference _ref)
        {
            reference = _ref;
            util = reference.GetUtilities;
        }


        public async Task ProcessFileAsync(string file, TextBlock tb, Button button)
        {
            processBtn = button;
            original_path = file;
            foreach (var f in Directory.GetParent(file).GetFiles())
            {
                if (f.Name.Contains(".done"))
                    should_run = false;
            }

            if (should_run)
            {
                if (reference.ConfigUtil.IsNetworkPath)
                {
                    string f = Path.Combine(reference.ConfigUtil.TempFolderDirectory, new FileInfo(file).Name);
                    var task = Task.Run(new Action(() =>
                    {
                        dis.Invoke(new Action(() =>
                        {
                            reference.Log.Add($"Copying {new FileInfo(file).Name} to {reference.ConfigUtil.TempFolderDirectory}");
                        }), DispatcherPriority.ContextIdle);

                        File.Copy(file, f);
                        file = f;
                        ready = true;
                        dis.Invoke(new Action(() =>
                        {
                            reference.Log.Add($"Coppied");
                        }), DispatcherPriority.ContextIdle);
                    }));
                    task.Wait();
                }

                dis.Invoke(new Action(() =>
                    {
                        reference.OriginalSize.Text = $"Original Size: {FileUtilities.AdjustedFileSize(file)}";
                    }), DispatcherPriority.ContextIdle);

                HasAborted = false;
                currentDirectory = Directory.GetParent(original_path).FullName;
                dis.Invoke(new Action(() =>
                {
                    reference.setLogBlock(tb);
                    reference.Log.Add("Processing File: " + new FileInfo(file).Name);
                }), DispatcherPriority.ContextIdle);
                this.file = file;

                var ffmpeg_file = Path.Combine(Environment.CurrentDirectory, "content", "ffmpeg.exe");
                var ffmpeg_qv = 24;
                if (!reference.ConfigUtil.UseEnclosedFolder)
                {
                    if (reference.ConfigUtil.TempFolderDirectory.Equals(""))
                    {
                        reference.Log.Add("Temp Folder is not set");
                        return;
                    }

                    string fileName = new FileInfo(file).Name;
                    string ext = new FileInfo(file).Extension;
                    fileName = fileName.Replace(ext, "").Replace(".", "");
                    fileName = Path.Combine(reference.ConfigUtil.TempFolderDirectory, fileName);
                    reference.Log.Add(fileName);
                    new_file = fileName + "_Encoded" + ext;
                }
                else
                    new_file = Path.Combine(Directory.GetParent(file).FullName, new FileInfo(file).Name.Replace(new FileInfo(file).Extension, "").Replace(".", "") + "_Encoded" + new FileInfo(file).Extension);
                if (File.Exists(new_file))
                    File.Delete(new_file);
                string command = " -hwaccel auto -i \"" + file + "\" -pix_fmt p010le -map 0:v -map 0:a -map_metadata 0 -c:v hevc_nvenc -rc constqp -qp " + ffmpeg_qv + " -b:v 0K -c:a aac -b:a 384k -movflags +faststart -movflags use_metadata_tags \"" + new_file + "\"";

                process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = ffmpeg_file;
                startInfo.Arguments = command;
                if (!reference.ConfigUtil.ShowEncodeConsole)
                {
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.CreateNoWindow = false;
                }
                else
                {
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    startInfo.CreateNoWindow = true;
                }
                process.Exited += new EventHandler(HandleExit);
                process.StartInfo = startInfo;
                process.Start();
                process.PriorityBoostEnabled = true;

                foreach (var f in Directory.GetParent(file).GetFiles())
                {
                    if (f.Name.Contains(".done"))
                    {
                        Abort(false, $"Encoder has Already processed {new FileInfo(file).Name}");
                    }
                }

                await Task.Run(new Action(() =>
                {
                    while (!process.HasExited && !HasAborted)
                    {
                        try
                        {
                            if (FileUtilities.FileSizeBytes(new_file) >= FileUtilities.FileSizeBytes(file))
                                Abort(true, $"Encoded File Size is larger than the original file. Moving on...");
                            dis.Invoke(new Action(() =>
                            {
                                reference.CurrentSize.Text = $"Current Size: {FileUtilities.AdjustedFileSize(new_file)}";
                            }), DispatcherPriority.ContextIdle);
                        }
                        catch (FileNotFoundException)
                        {

                        }
                        catch (Exception)
                        {

                        }
                    }
                }));

            }
        }

        private void HandleExit(object sender, EventArgs e)
        {
            if (HasAborted)
                return;
            try
            {
                float old_size = FileUtilities.FileSizeBytes(file), new_size = FileUtilities.FileSizeBytes(new_file);
                if (CheckFile(new_file))
                {
                    dis.Invoke(new Action(() =>
                    {
                        reference.Log.Add("Check was Successful");
                    }), DispatcherPriority.ContextIdle);

                    if (old_size <= new_size)
                    {
                        dis.Invoke(new Action(() =>
                        {
                            reference.Log.Add("Original File Size was smaller.");
                        }), DispatcherPriority.ContextIdle);
                        File.Delete(new_file);

                        dis.Invoke(new Action(() =>
                        {
                            reference.Log.Add("Removing Encoded File.");
                        }), DispatcherPriority.ContextIdle);
                    }
                    else
                    {
                        float difference = old_size - new_size;

                        dis.Invoke(new Action(() =>
                        {
                            reference.Log.Add("Encode Sucessful");
                            reference.Log.Add("You save " + FileUtilities.AdjustedFileSize(difference) + "!");
                            reference.Log.Add($"File Size Reduced by {Math.Round((new_size / old_size) * 100, 2)}%");
                            reference.CurrentSize.Text = "Finished!";
                            reference.OriginalSize.Text = $"File Size Reduced by {Math.Round((new_size / old_size) * 100, 2)}%";
                        }), DispatcherPriority.ContextIdle);
                        if (reference.ConfigUtil.OverWriteOriginal)
                        {
                            File.Delete(file);
                            File.Delete(original_path);
                            File.Move(new_file, original_path);

                            dis.Invoke(new Action(() =>
                            {
                                reference.Log.Add("Replacing Original...");
                            }), DispatcherPriority.ContextIdle);
                        }
                    }
                    string done = Path.Combine(currentDirectory, ".done");
                    File.Create(done);
                    FileInfo FI = new FileInfo(done);
                    FI.Attributes = FileAttributes.Hidden;
                }
                else
                {
                    dis.Invoke(new Action(() =>
                    {
                        reference.Log.Add("Check Failed!");
                        reference.Log.Add("File Corrupted.");
                    }), DispatcherPriority.ContextIdle);
                    File.Delete(new_file);
                    dis.Invoke(new Action(() =>
                    {
                        reference.Log.Add("Restoring Original.");
                    }), DispatcherPriority.ContextIdle);
                }
            }
            catch (FileNotFoundException ex)
            {
                dis.Invoke(new Action(() =>
                {
                    reference.Log.Add("File Couldn't Be Created!");
                    reference.Log.Add("Error: " + ex.StackTrace);
                    Console.WriteLine(ex.StackTrace);
                }), DispatcherPriority.ContextIdle);
            }
            catch (Exception ex)
            {
                dis.Invoke(new Action(() =>
                {
                    reference.Log.Add("Unknown Error: " + ex.StackTrace);
                    Console.WriteLine(ex.StackTrace);
                }), DispatcherPriority.ContextIdle);
            }
        }
        public void Abort(bool safe_abort, params string[] message)
        {
            try
            {
                HasAborted = true;
                process.Kill();

                if (safe_abort)
                {
                    string done = Path.Combine(currentDirectory, ".done");
                    File.Create(done);
                    FileInfo FI = new FileInfo(done);
                    FI.Attributes = FileAttributes.Hidden;
                }

                dis.Invoke(new Action(() =>
                {
                    processBtn.IsEnabled = true;
                    foreach (string s in message)
                    {
                        reference.Log.Add(s);
                    }
                }));
                if (new_file != string.Empty && File.Exists(new_file))
                {
                    System.Threading.Thread.Sleep(1 * 1000);
                    File.Delete(new_file);
                    if (reference.ConfigUtil.IsNetworkPath && ready && !file.Equals(original_path))
                        File.Delete(file);
                }
            }
            catch (Exception) { }
        }

        private bool CheckFile(string new_file)
        {
            dis.Invoke(new Action(() =>
            {
                reference.Log.Add("Checking File...");
            }), DispatcherPriority.ContextIdle);
            if (FileUtilities.FileSizeMB(new_file) >= 1) return true;
            return false;
        }

    }
}
