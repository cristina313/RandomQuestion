using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomQuestion.Classes
{
    public interface IDatabaseSettings
    {
       string ResourceQuestionsCollectionName { get;  }
       string ConnectionString { get;  }
       string DatabaseName { get;  }
    }
}
