using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using RandomQuestion.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                QuestionList questionList = questions.Find(question => true).FirstOrDefault();
                
                var resultArrayList = new ArrayList();
                foreach (var question in questionList.Questions)
                {
                    resultArrayList.Add(question.Text);
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
