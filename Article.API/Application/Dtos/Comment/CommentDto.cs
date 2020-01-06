using System;

namespace Article.API.Application.Dtos.Comment
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public string Body { get; set; }
    }
}
