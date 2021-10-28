using RandomQuestion.Classes.DataManagement;
using System;
using System.Collections;
using System.IO;

namespace RandomQuestion.Classes
{
    public class FileManager: IDataManager
    {
        IFileSettings settings;

        public FileManager(IFileSettings settings)
        {
            this.settings = settings;
        }

        public ArrayList ReadQuestions()
        {
            try
            {
                var resultArrayList = new ArrayList();
                resultArrayList.AddRange(File.ReadAllLines(settings.Path));
                return resultArrayList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
