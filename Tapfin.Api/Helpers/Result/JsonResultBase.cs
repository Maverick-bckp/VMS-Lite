using System.Net;

namespace Tapfin.Api.Helpers.Result
{
    public class JsonResultBase
    {
        public HttpStatusCode statusCode { get; set; } = HttpStatusCode.OK;
        public bool isSuccess { get; set; } = false;
        public DateTime server_Timestamp { get; set; } = DateTime.Now;
        public string? returnMessage { get; set; } = "";
        public bool? isValidationError { get; set; } = false;
        public bool? isTokenRefresh_Required { get; set; } = false;
        public bool? isLogin_Required { get; set; } = false;
        public dynamic? data { get; set; } = null;
    }
}
