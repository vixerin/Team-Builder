using System.Diagnostics;

namespace TeamBuilder.Common.Functional
{
    internal sealed class ResultCommonLogic
    {
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly string _error;

        public string Error
        {
            [DebuggerStepThrough]
            get
            {
                if (IsSuccess)
                    throw new InvalidOperationException("There is no error message for success.");

                return _error;
            }
        }

        [DebuggerStepThrough]
        public ResultCommonLogic(bool isFailure, string error)
        {
            if (isFailure)
            {
                if (string.IsNullOrEmpty(error))
                    throw new ArgumentNullException(nameof(error), "There must be error message for failure.");
            }
            else
            {
                if (error != null)
                    throw new ArgumentException("There should be no error message for success.", nameof(error));
            }

            IsFailure = isFailure;
            _error = error;
        }
    }


    public struct Result
    {
        private readonly Exception _exception;
        private const string _successMessage = "Sukces";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly ResultCommonLogic _logic;

        public bool IsFailure => _logic.IsFailure;
        public bool IsSuccess => _logic.IsSuccess;
        public string Error => _logic.Error;
        public string Message { get; }

        [DebuggerStepThrough]
        private Result(bool isFailure, string error, string message)
        {
            _logic = new ResultCommonLogic(isFailure, error);
            _exception = null;
            Message = isFailure ? "" : message;
        }

        [DebuggerStepThrough]
        private Result(bool isFailure, string error, string message, Exception exception) : this(isFailure, error, message)
        {
            _exception = exception;
        }

        [DebuggerStepThrough]
        public static Result Ok()
        {
            return new Result(false, null, _successMessage);
        }

        [DebuggerStepThrough]
        public static Result Ok(string message)
        {
            return new Result(false, null, message ?? _successMessage);
        }

        [DebuggerStepThrough]
        public static Result Fail(string error)
        {
            return new Result(true, error, null);
        }

        [DebuggerStepThrough]
        public static Result Fail(string error, Exception exception)
        {
            return new Result(true, error, null, exception);
        }

        [DebuggerStepThrough]
        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(false, value, null, _successMessage);
        }

        [DebuggerStepThrough]
        public static Result<T> Ok<T>(T value, string message)
        {
            return new Result<T>(false, value, null, message ?? _successMessage);
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string error)
        {
            return new Result<T>(true, default(T), error, null);
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string error, Exception exception)
        {
            return new Result<T>(true, default(T), error, null, exception);
        }

        /// <summary>
        /// Returns first failure in the list of <paramref name="results"/>. If there is no failure returns success.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static Result FirstFailureOrSuccess(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.IsFailure)
                    return Fail(result.Error);
            }

            return Ok();
        }

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list. Error messages are separated by <paramref name="messagesSeparator"/>. 
        /// If there is no failure returns success.
        /// </summary>
        /// <param name="messagesSeparator">Separator for error messages.</param>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        private static Result Combine(string messagesSeparator, params Result[] results)
        {
            var failedResults = results.Where(x => x.IsFailure).ToList();
            var successResults = results.Where(x => x.IsSuccess).ToList();

            if (!failedResults.Any())
            {
                string successMessage = string.Join(messagesSeparator, successResults.Select(x => x.Message).ToArray());
                return Ok(successMessage);
            }

            string errorMessage = string.Join(messagesSeparator, failedResults.Select(x => x.Error).ToArray());
            return Fail(errorMessage, failedResults.First()._exception);
        }

        [DebuggerStepThrough]
        public static Result Combine(params Result[] results)
        {
            return Combine("\r\n", results);
        }

        public void Done()
        {
        }
    }

    public struct Result<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly ResultCommonLogic _logic;
        private readonly Exception _exception;

        public bool IsFailure => _logic.IsFailure;
        public bool IsSuccess => _logic.IsSuccess;
        public string Error => _logic.Error;
        public string Message { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly T _value;

        public T Value
        {
            [DebuggerStepThrough]
            get
            {
                if (!IsSuccess)
                {
                    if (_exception == null)
                        throw new InvalidOperationException("There is no value for failure.\r\n" + Error);

                    throw _exception;
                }

                return _value;
            }
        }

        [DebuggerStepThrough]
        internal Result(bool isFailure, T value, string error, string message)
        {
            if (!isFailure && value == null)
                throw new ArgumentNullException(nameof(value));

            _logic = new ResultCommonLogic(isFailure, error);
            _value = value;
            _exception = null;
            Message = isFailure ? "" : message;
        }

        [DebuggerStepThrough]
        internal Result(bool isFailure, T value, string error, string message, Exception exception) : this(isFailure, value, error, message)
        {
            _exception = exception;
        }

        public Result ToResult()
        {
            if (IsSuccess)
                return Result.Ok(Message);

            return Result.Fail(Error, _exception);
        }

        public static implicit operator Result(Result<T> result)
        {
            if (result.IsSuccess)
                return Result.Ok();
            else
                return Result.Fail(result.Error);
        }

        public void Done()
        {
        }
    }
}
