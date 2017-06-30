using FileExtensionChanger.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExtensionChanger.UI.Windows
{
    public partial class FileExtensionChangerMain : Form
    {
        public FileExtensionChangerMain()
        {
            InitializeComponent();
        }

        private async Task<FileExtensionChangeResult> Start() {
            var options = new FileExtensionChangeOptions
            {
                Filenames = filenamesToChange.Text
                         .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                         .Where(s => !string.IsNullOrWhiteSpace(s))
                         .ToArray(),
                NewExtension = ".config",
                Strategy = ChangeExtensionStrategy.ChangeExistingExtension
            };

            var fileExtensionChange = new FileExtensionChange(options);
            //var newFName = fileExtensionChange.GetNewFilename(@"C:\inetpub\wwwroot\sitecore01\Website\App_Config\Commands.config.config.disabled");
            return await fileExtensionChange.Start();
        }
        
        private async void changeButton_Click(object sender, EventArgs e)
        {
            var result = await Start();
            foreach (var itemResult in result.ResultItems) {
                if (itemResult.Succeeded)
                {
                    Console.WriteLine(string.Format("File {0} renamed to {1}",
                        itemResult.Filename, itemResult.NewFilename));
                }
                else {
                    Console.WriteLine(string.Format("Failed to rename file {0} to {1}. Reason: {2}",
                        itemResult.Filename, itemResult.NewFilename, itemResult.Reason));
                }
            }
            //Console.WriteLine(newFName);

        }
    }
}
