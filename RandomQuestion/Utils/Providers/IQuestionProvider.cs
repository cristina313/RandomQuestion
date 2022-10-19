using RandomQuestion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomQuestion.Utils.Providers
{
    public interface IQuestionsProvider
    {
        QuestionsList GetQuestions(string language);
    }
}
