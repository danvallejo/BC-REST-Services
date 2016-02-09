using System;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace HelloWorldService
{
    public class AuthenticatorAttribute : AuthorizationFilterAttribute
    {
        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            try
            {
                bool valid = false;

                var authorization = actionContext.Request.Headers.Authorization;
                if (authorization.Scheme == "Bearer")
                {
                    var tokenString = authorization.Parameter;
                    var token = TokenHelper.DecodeToken(tokenString);
                    if (token.Expires > DateTime.UtcNow)
                    {
                        valid = true;
                    }
                }

                if (!valid)
                {
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }

                return base.OnAuthorizationAsync(actionContext, cancellationToken);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }
    }
}