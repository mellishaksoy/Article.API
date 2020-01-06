using System;

namespace Article.API.Application.Dtos.Comment
{
    public class CommentUpdateDto
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
    }
}
