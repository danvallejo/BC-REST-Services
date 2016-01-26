using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Http.Filters;

namespace HelloWorldService
{
    public class LoggingAttribute : ActionFilterAttribute
    {
        private System.Diagnostics.Stopwatch stopwatch;

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var currentRequest = actionContext.Request;
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            stopwatch.Stop();
            var milliseconds = stopwatch.ElapsedMilliseconds;
            System.IO.File.AppendAllText(System.Web.HttpContext.Current.Server.MapPath("~/Logger.txt"),
                string.Format("{0} : Elapsed={1} Url={2}",
                System.DateTime.Now, stopwatch.Elapsed, actionExecutedContext.Request.RequestUri));
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}