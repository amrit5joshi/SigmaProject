namespace SigmaProject.Models.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
        public static Result<T> Success(T value) => new() { IsSuccess = true, Value = value };
        public static Result<T> Error(string errorMessage) => new() { IsSuccess = false, ErrorMessage = errorMessage };
        public static Result<T> ValidationError(IEnumerable<string> errorMessage) => new() { IsSuccess = false, ValidationErrors = errorMessage };
    }
}