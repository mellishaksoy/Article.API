using Article.API.Application.Dtos.Comment;
using Article.API.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Application.Services.Comment
{
    public interface ICommentService
    {
        Task<CommentDto> GetByIdAsync(Guid id);
        Task<Paged<CommentDto>> GetAllAsync(CommentFilterDto filterDto);
        Task<CommentDto> AddAsync(CommentCreateDto dto);
        Task<CommentDto> UpdateAsync(CommentUpdateDto dto);
        Task DeleteAsync(CommentDeleteDto dto);
    }
}
