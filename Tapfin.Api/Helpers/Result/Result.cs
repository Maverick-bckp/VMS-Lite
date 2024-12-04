using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Tapfin.Api.Helpers.Result
{
    [NotMapped]
    public class Result : JsonResultBase
    {
        public Result AddReturnData(HttpStatusCode statusCode, dynamic? data = null, string? message = null)
        {
            this.statusCode = statusCode;
            this.returnMessage = message;
            this.data = data;

            return this;
        }

        public Result AddValidationError(HttpStatusCode statusCode, dynamic? data = null, string? message = null)
        {
            this.statusCode = statusCode;
            this.returnMessage = message;
            this.isValidationError = true;
            this.data = data;

            return this;
        }
    }
}
