using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomQuestion.Classes
{
    public class FileManager
    {
        public static ArrayList ReadFromFile(string path)
        {
            try
            {
                var resultArrayList = new ArrayList();
                resultArrayList.AddRange(File.ReadAllLines(path));
                return resultArrayList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
