using Autofac.Integration.WebApi;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FestiMS
{
    public class LoggingActionFilter : IAutofacActionFilter
    {
        public LoggingActionFilter()
        {
        }

        public Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            Debug.Write(actionContext.ActionDescriptor.ActionName);
            return Task.FromResult(0);
        }

        public Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            Debug.Write(actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
            return Task.FromResult(0);
        }
    }
}