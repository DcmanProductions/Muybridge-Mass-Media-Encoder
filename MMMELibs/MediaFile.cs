using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMMELibs
{
    public class MediaFile
    {
        public int ID { get; set; }
        public string FilePath { get; set; }
        public long Size { get; set; }
        public string FileName
        {
            get
            {
                return new FileInfo(FilePath).Name;
            }
        }
    }
}
