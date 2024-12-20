namespace SigmaProject.Models.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> ValidationErrors { get; set; }
        public static Result<T> Success(T value) => new() { IsSuccess = true, Value = value };
        public static Result<T> Error(string errorMessage) => new() { IsSuccess = false, ErrorMessage = errorMessage };
        public static Result<T> ValidationError(List<string> errorMessage, T value) => new() { IsSuccess = false, ValidationErrors = errorMessage, Value = value };
    }
}