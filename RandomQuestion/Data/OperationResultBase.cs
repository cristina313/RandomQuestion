using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomQuestion.Data
{
    public class OperationResult
    {
        public bool IsSuccess { get; }
        public string Error { get; private set; }
        public Exception Exception { get; private set; }
        public bool IsFailure => !IsSuccess;

        public OperationResult(bool success, string error, Exception exception)
        {
            if (success && error != string.Empty)
            {
                Exception = new InvalidOperationException();
            }

            if (!success && error == string.Empty)
            {
                Exception = new InvalidOperationException();
            }

            IsSuccess = success;
            Error = error;
            Exception = exception;
        }

        public static OperationResult Cancel()
        {
            return new OperationResult(false, "", null);
        }

        public static OperationResult<T> Cancel<T>()
        {
            return new OperationResult<T>(default, false, "", null);
        }

        public static OperationResult Fail(string message)
        {
            return new OperationResult(false, message, null);
        }

        public static OperationResult Fail(string message, Exception ex)
        {
            return new OperationResult(false, message, ex);
        }

        public static OperationResult<T> Fail<T>(string message)
        {
            return new OperationResult<T>(default, false, message, null);
        }

        public static OperationResult<T> Fail<T>(string message, Exception ex)
        {
            return new OperationResult<T>(default, false, message, ex);
        }

        public static OperationResult Ok()
        {
            return new OperationResult(true, string.Empty, null);
        }

        public static OperationResult<T> Ok<T>(T value)
        {
            return new OperationResult<T>(value, true, string.Empty, null);
        }
    }
}
