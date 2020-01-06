using Article.API.Domain.Common;
using System;

namespace Article.API.Application.Dtos.Comment
{
    public class CommentFilterDto: OrdinatedPageDto
    {
        public Guid ArticleId { get; set; }
    }
}
