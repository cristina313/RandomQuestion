using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomQuestion.Classes
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ResourceQuestionsCollectionName => ConfigurationManager.AppSettings["resQuestions"];
        public string ConnectionString => ConfigurationManager.ConnectionStrings["dev"].ConnectionString;
        public string DatabaseName => ConfigurationManager.AppSettings["databaseName"];
    }
}
