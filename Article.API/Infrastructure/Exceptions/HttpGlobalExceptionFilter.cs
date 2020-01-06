using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace Article.API.Infrastructure.Exceptions
{
    public class HttpGlobalExceptionFilter<T, TLocalizer> : IExceptionFilter where T : Enum
    {
        private readonly IHostingEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter<T, TLocalizer>> _logger;
        private readonly IStringLocalizer<TLocalizer> _localizer;

        public HttpGlobalExceptionFilter(IHostingEnvironment env,
                                         ILogger<HttpGlobalExceptionFilter<T, TLocalizer>> logger,
                                         IStringLocalizer<TLocalizer> localizer)
        {
            _env = env;
            _logger = logger;
            _localizer = localizer;
        }

        public void OnException(ExceptionContext context)
        {
            JsonErrorResponse json;

            var userLangs = context.HttpContext.Request.Headers["Accept-Language"].ToString();
            var firstLang = userLangs.Split(',').FirstOrDefault();
            var appliedLang = string.IsNullOrEmpty(firstLang) ? "en-US" : firstLang;
            var culture = new CultureInfo(appliedLang);

            if (context.Exception.GetType().BaseType == typeof(BaseException<T>))
            {
                var ex = ((BaseException<T>)context.Exception);
                var code = ex.Code.ToString("d");



                var message = _localizer.WithCulture(culture)[ex.Code.ToString()].ToString();



                if (ex.Arguments != null)
                {
                    message = String.Format(message, ex.Arguments);
                }

                json = new JsonErrorResponse
                {
                    Code = code,
                    Messages = new List<string>() { message }
                };

               
                if (_env.IsDevelopment())
                {
                    json.DeveloperMessage = !string.IsNullOrEmpty(ex.Message) ? ex.Message.ToString() : ex.InnerException?.ToString();
                }

                _logger.LogError(new EventId(context.Exception.HResult),
                                  ex.InnerException,
                                  code + " " + message + " " + ex.SerializedModel);
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                _logger.LogError(new EventId(context.Exception.HResult),
                                context.Exception,
                                context.Exception.Message);

                json = new JsonErrorResponse
                {
                    Code = "~",
                    Messages = new[] { _localizer.WithCulture(culture)["AnErrorOccured"].ToString() }
                };

                if (_env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception?.ToString();
                }

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.ExceptionHandled = true;
        }

        public class InternalServerErrorObjectResult : ObjectResult
        {
            public InternalServerErrorObjectResult(object error)
                : base(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError;
            }
        }

        private class JsonErrorResponse
        {
            public string Code { get; set; }
            public IList<string> Messages { get; set; }
            public object DeveloperMessage { get; set; }
        }
    }
}
