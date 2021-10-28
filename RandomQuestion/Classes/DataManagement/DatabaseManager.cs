using MongoDB.Driver;
using RandomQuestion.Models;
using System;
using System.Collections;
using System.Linq;

namespace RandomQuestion.Classes
{
    public class DatabaseManager : IDataManager
    {
        IDatabaseSettings settings;
        public DatabaseManager(IDatabaseSettings settings)
        {
            this.settings = settings;
        }

        public ArrayList ReadQuestions()
        {
            try
            {
                var client = new MongoClient(MongoClientSettings.FromConnectionString(settings.ConnectionString));
                var database = client.GetDatabase(settings.DatabaseName);
                var questions = database.GetCollection<QuestionList>(settings.ResourceQuestionsCollectionName);

                if (questions == null)
                {
                    database.CreateCollection(settings.ResourceQuestionsCollectionName);
                    questions = database.GetCollection<QuestionList>(settings.ResourceQuestionsCollectionName);
                }

                QuestionList questionList = questions.Find(question => true).FirstOrDefault();

                var resultArrayList = new ArrayList();
                if (questionList != null)
                {
                    foreach (var question in questionList.Questions)
                    {
                        resultArrayList.Add(question.Text);
                    }

                }

                return resultArrayList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
