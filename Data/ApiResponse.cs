using WebApplication1.Interfaces;

namespace WebApplication1.Data
{
    public class ApiResponse<T> 
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(T _data, int statusCode = 200, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Data = _data;
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Ok",
                400 => "You have made a bad request",
                401 => "You are not authorized",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Unknown Error"
            };
        }
    }
}
