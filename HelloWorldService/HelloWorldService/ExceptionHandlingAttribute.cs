using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace HelloWorldService
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var response = new
            {
                Status = "error",
                Message = actionExecutedContext.Exception.Message,
            };
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new ObjectContent(response.GetType(), response, new System.Net.Http.Formatting.JsonMediaTypeFormatter())
            };
            throw new HttpResponseException(httpResponseMessage);
        }
    }

}