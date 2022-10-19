using System;

namespace RandomQuestion.Data
{
    public class OperationResult<T> : OperationResult
    {
        private readonly T _value;
        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException();

                return _value;
            }
        }

        protected internal OperationResult(T value, bool isSuccess, string error, Exception exception)
            : base(isSuccess, error, exception)
        {
            _value = value;
        }
    }
}
