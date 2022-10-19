using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomQuestion.Data
{
    public interface IDataService
    {
        OperationResult<T> Read<T>(string path);
        OperationResult Write<T>(string path, T value);
        OperationResult Delete(string path);
        bool Exists(string path);
    }
}
