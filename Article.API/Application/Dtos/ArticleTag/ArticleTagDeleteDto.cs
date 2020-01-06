using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Application.Dtos.ArticleTag
{
    public class ArticleTagDeleteDto
    {
        public Guid ArticleId { get; set; }
        public Guid Id { get; set; }
    }
}
