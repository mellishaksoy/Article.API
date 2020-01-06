using Article.API.Application.Dtos.Article;
using Article.API.Application.Dtos.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Application.Dtos.ArticleTag
{
    public class ArticleTagDto
    {
        public Guid Id { get; set; }
        
        public TagDto Tag { get; set; }
    }
    
}
