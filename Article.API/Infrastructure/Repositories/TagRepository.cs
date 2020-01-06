using Article.API.Application.Dtos.Tag;
using Article.API.Domain;
using Article.API.Domain.Common;
using Article.API.Infrastructure.Contexts.ArticleEntity;
using Article.API.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        #region Private Fields
        private readonly ArticlesContext _ctx;
        #endregion

        #region Constructor
        public TagRepository(ArticlesContext ctx)
        {
            _ctx = ctx;
        }
        #endregion

        public async Task<Tag> AddAsync(Tag entity)
        {
            _ctx.Tags.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Tag entity)
        {
            _ctx.Tags.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Paged<Tag>> GetTagsAsync(TagFilterDto tagFilterDto)
        {
            var query = _ctx.Tags.Include(x => x.ArticleTags).Where(x => !x.IsDeleted);
            
            var totalPagesCount = query.CalculatePageCount(tagFilterDto.PageSize);

            var tags = await query.AsQueryable().OrderBy(tagFilterDto.Ordination)
                                    .SkipTake(tagFilterDto.PageIndex, tagFilterDto.PageSize)
                                    .ToListAsync();

            return new Paged<Domain.Tag>
            {
                List = tags,
                PageIndex = tagFilterDto.PageIndex,
                PageSize = tagFilterDto.PageSize,
                TotalPageCount = totalPagesCount
            };
        }

        public async Task<Tag> GetByIdAsync(Guid id)
        {
            var result = await _ctx.Tags.Include(c => c.ArticleTags).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            
            return result;
        }

        public async Task<Tag> UpdateAsync(Tag entity)
        {
            _ctx.Tags.Update(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> CheckAllTagIdsExist(List<Guid> tagIds)
        {
            var tags = await _ctx.Tags.Where(x => !x.IsDeleted).Select(x => x.Id).ToListAsync();
            var notExist = tagIds.Except(tags).ToList();
            return !notExist.Any();

        }
    }
}
