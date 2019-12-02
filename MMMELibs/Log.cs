using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace MMMELibs
{

    public class Log<String> : List<string>
    {

        private TextBlock LogBlock = null;
        private ScrollViewer ConsoleOutputScrView;
        private Utilities utilities;
        private Reference reference;
        public Log(Reference _ref)
        {
            reference = _ref;
            utilities = reference.GetUtilities;
        }

        public void setLogBlock(TextBlock block)
        {
            LogBlock = block;
        }

        public TextBlock getLogBlock()
        {
            return LogBlock;
        }

        public void setScrollView(ScrollViewer view)
        {
            ConsoleOutputScrView = view;
        }

        public ScrollViewer getScrollView()
        {
            return ConsoleOutputScrView;
        }

        public void Close()
        {
            try
            {
                var path = Path.Combine(Directory.GetParent(reference.LogFileLocation).FullName, $"{DateTime.Now.ToString().Replace(":", "-").Replace("/", "-")}.log");
                File.Move(reference.LogFileLocation, path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public new void Add(params string[] message)
        {
            if (getLogBlock() == null)
                return;
            string now = DateTime.Now.ToString();
            try
            {
                foreach (string obj in message)
                {
                    if (obj.GetType().Equals(typeof(string)))
                    {
                        base.Add(obj);
                        var consoleOut = new StringWriter();
                        Console.SetOut(consoleOut);
                        base.Add(consoleOut.ToString());
                        string data = now + ": " + obj;
                        Console.WriteLine(data);
                        getLogBlock().Text += consoleOut.ToString() + "\n";
                        getScrollView().ScrollToBottom();
                        ConfigUtilities config = new ConfigUtilities(reference);
                        string log_path = reference.LogLocation;
                        config.Write(data, "latest.log", log_path, false);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(now + ": " + e.Message);
                Console.WriteLine(now + ": " + e.StackTrace);
            }
        }
    }
}
