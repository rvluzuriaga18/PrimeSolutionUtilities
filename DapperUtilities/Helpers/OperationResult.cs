using System;

namespace DapperUtilities.Helpers
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }

        public OperationResult()
        {
        }

        public OperationResult(bool iSuccess, T result)
        {
            this.IsSuccess = iSuccess;
            this.Result = result;
        }

        public OperationResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }

        public OperationResult(String message)
        {
            this.Message = message;
        }

        public static OperationResult<T> Success()
        {
            return new OperationResult<T>(true);
        }

        public static OperationResult<T> Success(T result)
        {
            return new OperationResult<T>(true, result);
        }

        public static OperationResult<T> Failed(string message)
        {
            return new OperationResult<T>(message);
        }
    }
}
