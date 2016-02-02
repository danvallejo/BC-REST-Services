using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace HelloWorldService
{
    public class IPExcludeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            string ipExcludedAddress = ConfigurationManager.AppSettings["ExcludedIPAddress"];

            if (ipExcludedAddress.Contains(ipAddress))
            {
                var response = new
                {
                    Status = "No Way",
                };
                var httpResponseMessage = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Content = new ObjectContent(response.GetType(), response, new System.Net.Http.Formatting.JsonMediaTypeFormatter())
                };
                throw new HttpResponseException(httpResponseMessage);
            }

            base.OnActionExecuting(actionContext);
        }
    }
}