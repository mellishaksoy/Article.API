using Article.API.Application.Dtos.Comment;
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
    public class CommentRepository : ICommentRepository
    {
        #region Private Fields
        private readonly ArticlesContext _ctx;
        #endregion

        #region Constructor
        public CommentRepository(ArticlesContext ctx)
        {
            _ctx = ctx;
        }
        #endregion

        public async Task<Comment> AddAsync(Comment entity)
        {
            _ctx.Comments.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Comment entity)
        {
            _ctx.Comments.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            var result = await _ctx.Comments.Include(c => c.Article).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            return result;
        }

        public async Task<Paged<Comment>> GetCommentsAsync(CommentFilterDto filterDto)
        {
            var query = _ctx.Comments.Include(x => x.Article).Where(x => !x.IsDeleted);

            if(filterDto.ArticleId != Guid.Empty)
            {
                query = query.Where(x => x.ArticleId == filterDto.ArticleId);
            }

            var totalPagesCount = query.CalculatePageCount(filterDto.PageSize);

            var comments = await query.AsQueryable().OrderBy(filterDto.Ordination)
                                    .SkipTake(filterDto.PageIndex, filterDto.PageSize)
                                    .ToListAsync();

            return new Paged<Comment>
            {
                List = comments,
                PageIndex = filterDto.PageIndex,
                PageSize = filterDto.PageSize,
                TotalPageCount = totalPagesCount
            };
        }

        public async Task<Comment> UpdateAsync(Comment entity)
        {
            _ctx.Comments.Update(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }
    }
}
