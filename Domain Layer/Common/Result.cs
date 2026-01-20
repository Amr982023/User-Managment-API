using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Common
{
    public class Result <T> where T : class
    {
        public T _Value { get; }
        public bool _IsSuccess { get; }
        public bool Isfailure => !_IsSuccess;
        public string? _ErrorMessage { get; }

        public Result(T Value , bool IsSuccess, string ErrorMessage) 
        {
            if (IsSuccess && !string.IsNullOrEmpty(ErrorMessage))
            {
                throw new InvalidOperationException("A successful result cannot have an error message.");
            }
            if (!IsSuccess && string.IsNullOrEmpty(ErrorMessage))
            {
                throw new InvalidOperationException("A failed result must have an error message.");
            }

            _IsSuccess = IsSuccess;
            _ErrorMessage = ErrorMessage;
            _Value = Value;
        }

        public static Result<T> Success(T Value) => new Result<T>(Value ,true, string.Empty);

        public static Result<T> Failure(string errorMessage) => new Result<T>(default,false, errorMessage);

    }
}
