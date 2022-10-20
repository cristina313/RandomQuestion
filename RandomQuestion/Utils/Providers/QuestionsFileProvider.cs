using Microsoft.Toolkit.Mvvm.DependencyInjection;
using RandomQuestion.Data;
using RandomQuestion.Models;

namespace RandomQuestion.Utils.Providers
{
    public class QuestionsFileProvider : IQuestionsProvider
    {
        private readonly IDataService dataService;
        private string directoryPath;
        public QuestionsFileProvider(IDataService dataService, QuestionsProviderOptions options)
        {
            this.dataService = dataService;
            this.directoryPath = options.DirectoryPath;
        }

        public QuestionsList GetQuestions(string language)
        {
            var questions = new QuestionsList();
            var dataResult = dataService.Read<QuestionsList>(@$"{directoryPath}\q-{language}.json");

            if (dataResult.IsSuccess)
                questions = dataResult.Value;

            return questions;
        }
    }
}
