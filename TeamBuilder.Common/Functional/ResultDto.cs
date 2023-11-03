namespace TeamBuilder.Common.Functional
{
    public struct ResultDto
    {
        public bool IsFailure { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }

    public struct ResultDto<T>
    {
        public bool IsFailure { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public T Value { get; set; }
    }
}
