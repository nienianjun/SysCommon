using Sys.Common.Exception;
using Sys.Common.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Sys.Common.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext httpContext)
        {
            //Trace.TraceError("异常: {0}", context.Exception.Message);
            //Trace.TraceError("请求 URI: {0}", context.Request.RequestUri);
            Dictionary<string, Object> content = new Dictionary<string, Object>();
            if (httpContext.Exception is SysException)
            {
                SysException sysException = (SysException)httpContext.Exception;
                content.Add("StatusCode", sysException.StatusCode);
                content.Add("Message", sysException.Message);
            }
            else
            {
                content.Add("StatusCode", 500);
                content.Add("Message", "未知错误：" + httpContext.Exception.Message);
                content.Add("ErrorDesc", httpContext.Exception.ToString());
            }
            if (httpContext.Response == null)
            {
                httpContext.Response = new HttpResponseMessage(HttpStatusCode.OK); 
            }
            httpContext.Response.Content = new System.Net.Http.StringContent(StringHelper.ToJson(content));
        }
    }
}