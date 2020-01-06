using Article.API.Domain.Common;
using Article.API.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Controllers
{
    public abstract class BaseController : Controller
    {
        public OkObjectResult PagedOk<T>(T TEntity, OrdinatedPageDto ordinatedPageDto, int totalCount)
        {
            if (ordinatedPageDto.PageIndex > 1)
            {
                Response.Headers.AddParameter(Request, "X-Paging-Previous-Link", ordinatedPageDto.Ordination, ordinatedPageDto.PageSize, ordinatedPageDto.PageIndex - 1);
            }
            if (ordinatedPageDto.PageIndex < totalCount)
            {
                Response.Headers.AddParameter(Request, "X-Paging-Next-Link", ordinatedPageDto.Ordination, ordinatedPageDto.PageSize, ordinatedPageDto.PageIndex + 1);
            }
            return Ok(TEntity);
        }
    }
}
