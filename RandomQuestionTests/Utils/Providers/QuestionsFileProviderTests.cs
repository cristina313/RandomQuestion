using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomQuestion.Data;

namespace RandomQuestion.Utils.Providers.Tests
{
    [TestClass]
    public class QuestionsFileProviderTests
    {
        [TestMethod]
        public void GetRoQuestions_JsonFile_SuccessfulRead()
        {
            //Arrange
            var lang = "ro";
            var dirPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Assets\\Files\\";
            var qProvider = new QuestionsFileProvider(new JsonFileService(), new QuestionsProviderOptions { DirectoryPath = dirPath });

            //Act
            var questionResult = qProvider.GetQuestions(lang);

            //Assert
            Assert.IsTrue(questionResult.Questions != null && questionResult.Questions.Count == 7);
        }
    }
}