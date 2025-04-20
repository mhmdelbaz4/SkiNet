namespace Core.Responses;
public class ExceptionResponse
{
    public ExceptionResponse(int statusCode, string error, string details)
    {
        StatusCode = statusCode;
        Error = error;
        Details = details;
    }
    public int StatusCode { get; set; }
    public string Error { get; set; }
    public string Details { get; set; }
}
