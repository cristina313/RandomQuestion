using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomQuestion.Classes.DataManagement
{
    public class FileSettings : IFileSettings
    {
        public string Path => ConfigurationManager.AppSettings["filePath"];
    }
}
