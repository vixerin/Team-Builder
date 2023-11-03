namespace TeamBuilder.Common.Functional
{
    public static class ResultExtensions
    {
        /// <summary>
        /// If Result is success, executes a method that has no parameters and does not return a value. 
        /// Result is passed through from previous statement to next.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action">()= {}</param>
        /// <returns></returns>
        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        /// <summary>
        /// If Result is success, executes a method that has no parameters and returns new Result. 
        /// If Result is failure Result is passed through from previous statement to next.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func">()=> return Result</param>
        /// <returns></returns>
        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            if (result.IsFailure)
                return result;

            return func();
        }

        /// <summary>
        /// If Result is success, executes a method that has no parameters and returns new Result T.Ok() from return value T.
        /// If Result is failure result is convert to Result T and passed through from previous statement to next.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func">()=> return T</param>
        /// <returns></returns>
        public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return Result.Ok(func());
        }

        /// <summary>
        /// If Result is success, executes a method that has no parameters and returns new Result T.
        /// If Result is failure result is convert to Result T and passed through from previous statement to next.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func">()=> return Result T</param>
        /// <returns></returns>
        public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return func();
        }

        /// <summary>
        /// If Result is success, executes a method that has T parameter and returns new Result K.Ok() from return value K.
        /// If Result is failure result is convert from Result T to Result K and passed through from previous statement to next.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func">(T)=> return K</param>
        /// <returns></returns>
        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return Result.Ok(func(result.Value));
        }

        /// <summary>
        /// If Result is success, executes a method that has T parameter and returns new Result K.
        /// If Result is failure result is convert from Result T to Result K and passed through from previous statement to next.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func">(T)=> return Result K</param>
        /// <returns></returns>
        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, Result<K>> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return func(result.Value);
        }

        /// <summary>
        /// If Result is success, executes a method that has no parameter and returns new Result K.
        /// If Result is failure result is convert from Result T to Result K and passed through from previous statement to next.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func">()=> return Result K</param>
        /// <returns></returns>
        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<Result<K>> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return func();
        }

        /// <summary>
        /// If Result is success, executes a method that has T parameter and returns new Result.
        /// If Result is failure result is convert from Result T to Result and passed through from previous statement to next.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func">(T)=> return Result</param>
        /// <returns></returns>
        public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);

            return func(result.Value);
        }

        /// <summary>
        /// If Result is success, executes a method that has T parameter and does not return a value.
        /// Result is passed through from previous statement to next.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action">(T)=> {}</param>
        /// <returns></returns>
        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public static Result OnSuccess<T>(this Result<T> result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        /// <summary>
        /// If Result is failure, executes a method that has no parameter and does not return a value.
        /// Result is passed through from previous statement to next.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action">()=> {}</param>
        /// <returns></returns>
        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        /// <summary>
        /// If Result is failure, executes a method that has string parameter as Error and does not return a value.
        /// Result is passed through from previous statement to next.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action">(s)=> {}</param>
        /// <returns></returns>
        public static Result OnFailure(this Result result, Action<string> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        /// <summary>
        /// If Result is failure, executes a method that has no parameter and does not return a value.
        /// Result is passed through from previous statement to next.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action">()=> {}</param>
        /// <returns></returns>
        public static Result<T> OnFailure<T>(this Result<T> result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        /// <summary>
        /// If Result is failure, executes a method that has string parameter as Error and does not return a value.
        /// Result is passed through from previous statement to next.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action">(s)=> {}</param>
        /// <returns></returns>
        public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        /// <summary>
        /// If Result is failure, Result is passed through from previous statement to next.
        /// Executes predicate. If predicate result is true, returns Result.Ok(), if is false, returns Result.Fail() with given error message.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="predicate">()=> return bool</param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Result Ensure(this Result result, Func<bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);

            if (!predicate())
                return Result.Fail(errorMessage);

            return Result.Ok();
        }

        /// <summary>
        /// If Result is failure, Result is passed through from previous statement to next.
        /// Executes predicate. If predicate result is true, returns Result T.Ok(), if is false, returns Result T.Fail() with given error message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="predicate">(T)=> return bool</param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            if (!predicate(result.Value))
                return Result.Fail<T>(errorMessage);

            return Result.Ok(result.Value);
        }

        /// <summary>
        /// Converts from Result to Result T.
        /// If Result is success, executes a method that has no parameter and returns new Result T.Ok() from T return value.
        /// If Result is failure result is convert from Result to Result T and passed through from previous statement to next.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func">()=> return T</param>
        /// <returns></returns>
        public static Result<T> Map<T>(this Result result, Func<T> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return Result.Ok(func());
        }

        /// <summary>
        /// Converts from Result T to Result K.
        /// If Result is success, executes a method that has T parameter and returns new Result K.Ok() from K return value.
        /// If Result is failure result is convert from Result T to Result K and passed through from previous statement to next.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="result"></param>
        /// <param name="func">(T)=> return K</param>
        /// <returns></returns>
        public static Result<K> Map<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return Result.Ok(func(result.Value));
        }

        /// <summary>
        /// On both success or failure, executes a method that has Result parameter and returns T value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func">(result)=> return T</param>
        /// <returns></returns>
        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }

        /// <summary>
        /// On both success or failure, executes a method that has Result T parameter and returns T value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="result"></param>
        /// <param name="func">(result T)=> return K</param>
        /// <returns></returns>
        public static K OnBoth<T, K>(this Result<T> result, Func<Result<T>, K> func)
        {
            return func(result);
        }

        public static ResultDto ToDto(this Result result)
        {
            var dto = new ResultDto
            {
                IsSuccess = result.IsSuccess,
                IsFailure = result.IsFailure
            };
            if (result.IsFailure)
                dto.Error = result.Error;
            if (result.IsSuccess)
                dto.Message = result.Message;
            return dto;
        }

        public static ResultDto<T> ToDto<T>(this Result<T> result)
        {
            var dto = new ResultDto<T>
            {
                IsSuccess = result.IsSuccess,
                IsFailure = result.IsFailure
            };
            if (result.IsFailure)
                dto.Error = result.Error;
            if (result.IsSuccess)
            {
                dto.Value = result.Value;
                dto.Message = result.Message;
            }

            return dto;
        }

        public static Result FromDto(this ResultDto dto)
        {
            if (dto.IsSuccess)
            {
                var result =  Result.Ok(dto.Message);
                return result;
            }
            return Result.Fail(dto.Error);
        }

        public static Result<T> FromDto<T>(this ResultDto<T> dto)
        {
            if (dto.IsSuccess)
            {
                var result =  Result.Ok(dto.Value, dto.Message);
                return result;
            }

            return Result.Fail<T>(dto.Error);
        }
    }
}
