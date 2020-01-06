using Article.API.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Application.Dtos.Article
{
    public class ArticleFilterDto : OrdinatedPageDto
    {
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string Tag { get; set; }
    }
}
