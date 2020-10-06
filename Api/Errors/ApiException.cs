namespace Api.Errors
{
    public class ApiException  : ApiResponse
    {
        public string Details { get; set; }

        public ApiException(int statusCode, string details, string message = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}