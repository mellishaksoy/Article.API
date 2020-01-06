using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;

namespace Article.API.Infrastructure.Middlewares
{
    public class SerilogRequestLoggerMiddleware
    {
        readonly string TraceId = "Request-Trace-ID";
        readonly RequestDelegate _next;

        public SerilogRequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        private void CheckParameters(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if (!string.IsNullOrEmpty(httpContext.Request.Headers[TraceId]))
            {
                LogContext.PushProperty(TraceId, httpContext.Request.Headers[TraceId]);
            }
        }

        public async System.Threading.Tasks.Task Invoke(HttpContext httpContext)
        {
            CheckParameters(httpContext);
            await _next(httpContext);
        }
    }
}
