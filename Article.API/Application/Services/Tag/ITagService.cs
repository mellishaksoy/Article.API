using Article.API.Application.Dtos.Tag;
using Article.API.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Application.Services.Tag
{
    public interface ITagService
    {
        Task<TagDto> GetByIdAsync(Guid tagId);
        Task<Paged<TagDto>> GetAllAsync(TagFilterDto tagFilterDto);
        Task<TagDto> AddAsync(TagCreateDto dto);
        Task<TagDto> UpdateAsync(TagUpdateDto dto);
        Task DeleteAsync(TagDeleteDto dto);
    }
}
