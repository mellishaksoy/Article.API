using System;
using System.Collections.Generic;

namespace Article.API.Application.Dtos.Article
{
    public class ArticleCreateDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid CategoryId { get; set; }
        public List<Guid> TagIds { get; set; }
    }
}
