using Article.API.Application.Dtos.Comment;
using Article.API.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Domain
{
    public interface ICommentRepository : IRepository
    {
        Task<Comment> AddAsync(Comment entity);
        Task<Comment> GetByIdAsync(Guid id);
        Task DeleteAsync(Comment entity);
        Task<Comment> UpdateAsync(Comment entity);
        Task<Paged<Comment>> GetCommentsAsync(CommentFilterDto filterDto);
    }
}
