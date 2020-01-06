using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Application.Dtos.ArticleTag
{
    public class ArticleTagCreateDto
    {
        public Guid ArticleId { get; set; }
        public List<Guid> TagIds { get; set; }
    }
}
