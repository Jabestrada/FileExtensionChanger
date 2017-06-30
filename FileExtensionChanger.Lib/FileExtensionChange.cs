using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExtensionChanger.Lib
{
    public class FileExtensionChange
    {
        FileExtensionChangeOptions _options;

        public FileExtensionChange(FileExtensionChangeOptions options)
        {
            if (!options.NewExtension.StartsWith(".")) {
                options.NewExtension = "." + options.NewExtension;
            }
            _options = options;
        }

        public async Task<FileExtensionChangeResult> Start() {

            return await Task.Run(() => {
                var result = new FileExtensionChangeResult { Options = _options};
                foreach (var inputFile in _options.Filenames) {
                    var newFileName = GetNewFilename(inputFile);
                    if (string.IsNullOrWhiteSpace(newFileName))
                    {
                        result.ResultItems.Add(new FileExtensionChangeResultItem
                        {
                            Filename = inputFile,
                            NewFilename = newFileName,
                            Succeeded = false,
                            Reason = "Generated output filename is empty"
                        });
                        continue;
                    }
                    else if (File.Exists(newFileName)) {
                        result.ResultItems.Add(new FileExtensionChangeResultItem {
                            Filename = inputFile,
                            NewFilename = newFileName,
                            Succeeded = false,
                            Reason = "Output file already exists"
                        });
                        continue;
                    }
                    try
                    {
                        File.Move(inputFile, newFileName);
                        result.ResultItems.Add(new FileExtensionChangeResultItem
                        {
                            Filename = inputFile,
                            NewFilename = newFileName,
                            Succeeded = true
                        });
                    }
                    catch(Exception exc)
                    {
                        result.ResultItems.Add(new FileExtensionChangeResultItem
                        {
                            Filename = inputFile,
                            NewFilename = newFileName,
                            Succeeded = false,
                            Reason = exc.Message
                        });
                    }

                }
                return result;
            });
        }

        public string GetNewFilename(string inputFile) {
            var newFileName = string.Empty;
            // Get redundancy out of the way.
            if (Path.GetExtension(inputFile).ToLowerInvariant() == _options.NewExtension) {
                return inputFile;
            }

            switch (_options.Strategy)
            {
                case ChangeExtensionStrategy.ChangeExistingExtension:
                    newFileName = Path.ChangeExtension(inputFile, _options.NewExtension);
                    // e.g., myfile.config.disabled ends with myfile.config instead of myfile.config.config
                    while (Path.GetFileNameWithoutExtension(newFileName).EndsWith(_options.NewExtension)) {
                        newFileName = Path.GetFileNameWithoutExtension(newFileName);
                    }
                    break;
                case ChangeExtensionStrategy.AppendNewExtension:
                    newFileName = inputFile + _options.NewExtension;
                    break;
            }
            return newFileName;
        }
    }
}
