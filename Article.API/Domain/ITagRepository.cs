using Article.API.Application.Dtos.Tag;
using Article.API.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Domain
{
    public interface ITagRepository : IRepository
    {
        Task<Tag> AddAsync(Tag entity);
        Task<Tag> GetByIdAsync(Guid id);
        Task DeleteAsync(Tag entity);
        Task<Tag> UpdateAsync(Tag entity);
        Task<Paged<Tag>> GetTagsAsync(TagFilterDto tagFilterDto);
        Task<bool> CheckAllTagIdsExist(List<Guid> tagIds);
    }
}
