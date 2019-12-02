using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MMMELibs
{
    public class Reference
    {

        public Reference GetInstance => this;

        public Log<string> Log => new Log<string>(GetInstance);

        public ConfigUtilities GetConfig => new ConfigUtilities(GetInstance);

        public Utilities GetUtilities => new Utilities(GetInstance);

        private TextBlock LogBlock = null;
        private ScrollViewer sv = null;
        private TextBlock currentSize, originalSize;

        public Reference()
        {
        }

        public MediaFiles MediaFiles { get; } = new MediaFiles();

        public TextBlock CurrentSize
        {
            get;
            set;
        }

        public TextBlock OriginalSize
        {
            get;
            set;
        }

        private string _buildVersion = "N/A";
        public string BuildVersion
        {
            get
            {
                return _buildVersion;
            }
            set
            {
                _buildVersion = "build v." + value;
            }
        }

        public string InstallationFolder
        {
            get
            {
                return Environment.CurrentDirectory;
            }
        }


        private ConfigUtilities configutil;
        public ConfigUtilities ConfigUtil
        {
            get
            {
                return configutil;
            }
            set
            {

                configutil = value;
            }
        }


        private string configlocation = "";
        public string ConfigLocation
        {
            get
            {
                return configlocation;
            }
            set
            {
                string dir = Path.Combine(value, "Configurations");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                configlocation = dir;
            }
        }

        private string loglocation = "";
        public string LogLocation
        {
            get
            {
                return loglocation;
            }
            set
            {
                string dir = Path.Combine(value, "Logs");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                loglocation = dir;
            }
        }

        private string rootlocation = "";
        public string RootLocation
        {
            get
            {
                return rootlocation;
            }
            set
            {
                string dir = Path.Combine(value, "Muybridge", "MassMediaEncoder");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                LogLocation = dir;
                ConfigLocation = dir;
                rootlocation = dir;
            }
        }

        public string LogFileLocation
        {
            get
            {
                return Path.Combine(LogLocation, "latest.log");
            }
        }

        public string ConfigFileLocation
        {
            get
            {
                return Path.Combine(ConfigLocation, "default.config");
            }
        }


        public void setLogBlock(TextBlock block)
        {
            LogBlock = block;
            Log.setLogBlock(getLogBlock());
        }

        public ScrollViewer getScrollView()
        {
            return sv;
        }
        public void setScrollView(ScrollViewer block)
        {
            sv = block;
            Log.setScrollView(getScrollView());
        }

        public TextBlock getLogBlock()
        {
            return LogBlock;
        }

    }

}
