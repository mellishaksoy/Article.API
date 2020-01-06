using Article.API.Infrastructure.Exceptions;
using Article.API.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Infrastructure.Filters
{
    public class ArticleHttpGobalExceptionFilter : HttpGlobalExceptionFilter<ArticleErrorCodes, ErrorCodes>
    {
        public ArticleHttpGobalExceptionFilter(
                 IHostingEnvironment env,
                 ILogger<HttpGlobalExceptionFilter<ArticleErrorCodes, ErrorCodes>> logger,
                 IStringLocalizer<ErrorCodes> localizer) : base(env, logger, localizer)
        {

        }
    }
}
