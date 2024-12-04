using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Tapfin.Api.Filters
{
    public class ReturnResultFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            ContentResult contentResult = new ContentResult();

            #region
            /*------- Changing property values of API Response  Based on Result Status Code ---------*/

            var result = context.Result;

            if (result != null)
            {

                JObject jobj = JObject.Parse(JsonConvert.SerializeObject(result));
                var content = jobj.SelectToken("Value").ToString();

                var contentJobj = JObject.Parse(content);
                var statusCode = Convert.ToInt32(contentJobj["statusCode"]);
                var returnMessage = contentJobj["returnMessage"].ToString();

                if (statusCode == (int)HttpStatusCode.Unauthorized || returnMessage.Contains("Unauthorized"))
                {
                    contentJobj["statusCode"] = (int)HttpStatusCode.Unauthorized;
                    contentJobj["isSuccess"] = false;
                    contentJobj["isTokenRefresh_Required"] = true;
                    contentJobj["isLogin_Required"] = true;


                    contentResult.StatusCode = (int)HttpStatusCode.Unauthorized;
                    contentResult.Content = JsonConvert.SerializeObject(contentJobj);
                    contentResult.ContentType = "application/json";
                }
                else if (statusCode == (int)HttpStatusCode.NoContent)
                {
                    contentResult.StatusCode = (int)HttpStatusCode.OK;
                    contentResult.Content = JsonConvert.SerializeObject(contentJobj);
                    contentResult.ContentType = "application/json";
                }
                else if (statusCode == (int)HttpStatusCode.OK || statusCode == (int)HttpStatusCode.Created)
                {
                    contentJobj["isSuccess"] = true;

                    contentResult.StatusCode = statusCode;
                    contentResult.Content = JsonConvert.SerializeObject(contentJobj);
                    contentResult.ContentType = "application/json";
                }
                else
                {
                    contentResult.StatusCode = (int)statusCode;
                    contentResult.Content = JsonConvert.SerializeObject(contentJobj);
                    contentResult.ContentType = "application/json";
                }


                context.Result = contentResult;
            }
            #endregion
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
