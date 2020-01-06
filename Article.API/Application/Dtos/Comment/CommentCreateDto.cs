using System;

namespace Article.API.Application.Dtos.Comment
{
    public class CommentCreateDto
    {
        public Guid ArticleId { get; set; }
        public string Body { get; set; }
    }
}
