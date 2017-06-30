using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExtensionChanger.Lib
{
    public enum ChangeExtensionStrategy
    {
        AppendNewExtension,
        ChangeExistingExtension
    }


    public class FileExtensionChangeOptions
    {
        public string[] Filenames { get; set; }
        public string NewExtension { get; set; }
        public ChangeExtensionStrategy Strategy { get; set; }
    }
}
