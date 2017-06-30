using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExtensionChanger.Lib
{
    public class FileExtensionChangeResultItem {
        public string Filename { get; set; }
        public string NewFilename { get; set; }
        public bool Succeeded { get; set; }
        public string Reason { get; set; }
    }

    public class FileExtensionChangeResult
    {
        public FileExtensionChangeOptions Options;
        public List<FileExtensionChangeResultItem> ResultItems;

        public FileExtensionChangeResult()
        {
            ResultItems = new List<FileExtensionChangeResultItem>();
        }

    }
}
