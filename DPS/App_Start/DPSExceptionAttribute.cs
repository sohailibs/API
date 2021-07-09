using DPS.Models;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace DPS
{
    internal class DPSExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Common common = new Common();
            if (actionExecutedContext.Exception.GetType() == typeof(SqlException))
            {
                actionExecutedContext.ActionContext.ModelState.AddModelError("", "Sql Server service is not available");
                actionExecutedContext.Response = actionExecutedContext.Request
                        .CreateErrorResponse(HttpStatusCode.BadGateway, actionExecutedContext.ActionContext.ModelState);


                common.ExeceptionHandleWithMethodName(actionExecutedContext.Exception, "DSPExceptionAttribute Method Name: OnException First If Condition");

            }
            else if (actionExecutedContext.Exception.GetType() == typeof(System.Net.Mail.SmtpException))
            {
                actionExecutedContext.ActionContext.ModelState.AddModelError("", "Unable to send Email.");
                actionExecutedContext.Response = actionExecutedContext.Request
                        .CreateErrorResponse(HttpStatusCode.GatewayTimeout, actionExecutedContext.ActionContext.ModelState);

                common.ExeceptionHandleWithMethodName(actionExecutedContext.Exception, "DSPExceptionAttribute Method Name: OnException  If else Condition");
            }
            else
            {
                actionExecutedContext.Response =
                    actionExecutedContext.Request
                    .CreateErrorResponse(HttpStatusCode.InternalServerError, actionExecutedContext.Exception);


                common.ExeceptionHandleWithMethodName(actionExecutedContext.Exception, "DSPExceptionAttribute Method Name: OnException  else Condition");
            }
        }
    }
}