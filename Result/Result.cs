namespace PetGrubBakcend.Result
{
    public class Result<T>
    {
        public bool isSuccess {  get; set; }
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }

        public static Result<T> Success(T data) => new Result<T>
        {
            isSuccess = true,
            Data = data
        };

        public static Result<T> Failure(string errorMessage) => new Result<T>
        {
            isSuccess = false,
            ErrorMessage = errorMessage
        };
    }
}
