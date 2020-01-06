using Article.API.Application.Dtos.ArticleTag;
using Article.API.Application.Dtos.Category;
using Article.API.Application.Dtos.Comment;
using Article.API.Application.Dtos.Tag;
using System;
using System.Collections.Generic;

namespace Article.API.Application.Dtos.Article
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public CategoryDto Category { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
