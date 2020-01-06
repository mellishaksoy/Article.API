using Article.API.Application.Dtos.Category;
using Article.API.Domain;
using Article.API.Domain.Common;
using Article.API.Infrastructure.Contexts.ArticleEntity;
using Article.API.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        #region Private Fields
        private readonly ArticlesContext _ctx;
        #endregion

        #region Constructor
        public CategoryRepository(ArticlesContext ctx)
        {
            _ctx = ctx;
        }
        #endregion

        public async Task<Category> AddAsync(Category entity)
        {
            _ctx.Categories.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Category entity)
        {
            _ctx.Categories.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var result = await _ctx.Categories.Include(c => c.Articles).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            return result;
        }

        public async Task<Paged<Category>> GetCategoriesAsync(CategoryFilterDto filterDto)
        {
            var query = _ctx.Categories.Include(x => x.Articles).Where(x => !x.IsDeleted);

            var totalPagesCount = query.CalculatePageCount(filterDto.PageSize);

            var categories = await query.AsQueryable().OrderBy(filterDto.Ordination)
                                    .SkipTake(filterDto.PageIndex, filterDto.PageSize)
                                    .ToListAsync();

            return new Paged<Category>
            {
                List = categories,
                PageIndex = filterDto.PageIndex,
                PageSize = filterDto.PageSize,
                TotalPageCount = totalPagesCount
            };
        }

        public async Task<Category> UpdateAsync(Category entity)
        {
            _ctx.Categories.Update(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }
    }
}
