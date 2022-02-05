using System.Net;

namespace WorkloadCalculator.Domain.Responses
{
    public class ApiExceptionResponse : ApiExceptionBaseResponse
    {
        public string Details { get; set; }
        public ApiExceptionResponse(int statusCode, string message = null, string details = null) : base(
            (int) HttpStatusCode.InternalServerError, message)
        {
            Details = details;
        }

    }
}
