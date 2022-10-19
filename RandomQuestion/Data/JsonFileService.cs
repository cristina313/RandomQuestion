using Newtonsoft.Json;
using RandomQuestion.Utils;
using System;
using System.IO;


namespace RandomQuestion.Data
{
    public class JsonFileService : IDataService
    {
        public OperationResult Delete(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return OperationResult.Fail($"{path} {Constants.NOT_FOUND}");

                File.Delete(path);
                return OperationResult.Ok();
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"{Constants.OPERATION_EXCEPTION} - {nameof(Delete)}", ex);
            }
        }

        public OperationResult<T> Read<T>(string path)
        {
            try
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException($"{path} {Constants.NOT_FOUND}");
                var operationResult = OperationResult.Ok(JsonConvert.DeserializeObject<T>(File.ReadAllText(path)));
                return operationResult;
            }
            catch (Exception ex)
            {
                return OperationResult.Fail<T>($"{Constants.OPERATION_EXCEPTION} - {nameof(Read)}", ex);
            }
        }

        public OperationResult Write<T>(string path, T value)
        {
            try
            {
                var dirPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
                File.WriteAllText(path, JsonConvert.SerializeObject(value, Formatting.Indented));
                return OperationResult.Ok();
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"{Constants.OPERATION_EXCEPTION} - {nameof(Write)}", ex);
            }
        }

        public bool Exists(string path)
        {
            try
            {
                if (File.Exists(path))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
