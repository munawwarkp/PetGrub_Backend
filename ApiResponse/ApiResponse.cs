namespace PetGrubBakcend.ApiResponse
{
    public class ApiResponse<T>
    {
        public int StatusCode  {  get; set; }
        public string? Message {  get; set; }
        public T? Data { get; set; }
        public string ? Error {  get; set; }

        //public ApiResponse(int statusCode, string? message, T? data, string? error)
        //{
        //    StatusCode = statusCode;
        //    Message = message;
        //    Data = data;
        //    Error = error;
        //}

    }
}
