using System.Configuration;

namespace RandomQuestion.Classes
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ResourceQuestionsCollectionName => ConfigurationManager.AppSettings["resQuestions"];
        public string ConnectionString => ConfigurationManager.ConnectionStrings["dev"].ConnectionString;
        public string DatabaseName => ConfigurationManager.AppSettings["databaseName"];
    }
}
