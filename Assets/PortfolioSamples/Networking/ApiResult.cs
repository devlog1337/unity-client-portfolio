namespace PortfolioSamples.Networking
{
    public readonly struct ApiResult<T>
    {
        public bool IsSuccess { get; }
        public long StatusCode { get; }
        public string Error { get; }
        public T Data { get; }

        private ApiResult(bool isSuccess, long statusCode, string error, T data)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Error = error;
            Data = data;
        }

        public static ApiResult<T> Success(long statusCode, T data)
            => new(true, statusCode, null, data);

        public static ApiResult<T> Failure(long statusCode, string error)
            => new(false, statusCode, error, default);
    }
}
