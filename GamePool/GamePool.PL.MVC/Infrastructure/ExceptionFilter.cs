using System.Net;
using System.Web.Mvc;

namespace GamePool.PL.MVC.Infrastructure
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            exceptionContext.ExceptionHandled = true;
            exceptionContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
    }
}