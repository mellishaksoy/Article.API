using Article.API.Application.Dtos.Article;
using Article.API.Application.Dtos.Comment;
using Article.API.Domain.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Article.API.Domain
{
    public interface IArticleRepository : IRepository
    {
        Task<Article> AddAsync(Article entity);
        Task<Article> GetByIdAsync(Guid id);
        Task DeleteAsync(Article entity);
        Task<Article> UpdateAsync(Article entity);
        Task<Paged<Article>> GetArticlesAsync(ArticleFilterDto filterDto);

        Task<Article> AddTags(Article entity, List<Guid> tagIds);

        Task<Comment> AddAsync(Comment entity);
        Task<Comment> GetCommentByIdAsync(Guid id);
        Task DeleteAsync(Comment entity);
        Task<Comment> UpdateAsync(Comment entity);
        Task<Paged<Comment>> GetCommentsAsync(CommentFilterDto filterDto);
    }
}
