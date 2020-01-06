using Article.API.Application.Dtos.Article;
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
    public class ArticleRepository : IArticleRepository
    {
        #region Private Fields
        private readonly ArticlesContext _ctx;
        #endregion

        #region Constructor
        public ArticleRepository(ArticlesContext ctx)
        {
            _ctx = ctx;
        }
        #endregion

        public async Task<Domain.Article> AddAsync(Domain.Article entity)
        {
            _ctx.Articles.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Domain.Article entity)
        {
            var tags = _ctx.ArticleTags.Where(x => x.ArticleId == entity.Id).ToList();
            _ctx.ArticleTags.RemoveRange(tags);
            await _ctx.SaveChangesAsync();

            var comments = _ctx.Comments.Where(x => x.ArticleId == entity.Id).ToList();
            _ctx.Comments.RemoveRange(comments);
            await _ctx.SaveChangesAsync();

            _ctx.Articles.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Paged<Domain.Article>> GetArticlesAsync(ArticleFilterDto filterDto)
        {
            var query = _ctx.Articles.Include(x => x.Category).Include(x => x.Comments)
                .Include(x => x.ArticleTags).Where(x => !x.IsDeleted);

            var totalPagesCount = query.CalculatePageCount(filterDto.PageSize);

            if (!string.IsNullOrEmpty(filterDto.CategoryName))
            {
                query = query.Where(x => x.Category != null && x.Category.Name.Contains(filterDto.CategoryName));
            }

            if (!string.IsNullOrEmpty(filterDto.Title))
            {
                query = query.Where(x => x.Title.Contains(filterDto.Title));
            }



            var articles = await query.AsQueryable().OrderBy(filterDto.Ordination)
                                    .SkipTake(filterDto.PageIndex, filterDto.PageSize)
                                    .ToListAsync();

            var tags = await _ctx.ArticleTags.Include(x => x.Tag).ToListAsync();

            articles.ForEach(a =>
            {
                a.ArticleTags = tags.Where(x => x.ArticleId == a.Id).ToList();
                a.Tags = a.ArticleTags.Select(x => x.Tag).ToList();
            });
            

            if (!string.IsNullOrEmpty(filterDto.Tag))
            {
                articles = articles.Where(x => x.Tags.Any(t => t.Name.Contains(filterDto.Tag))).ToList();
            }

            return new Paged<Domain.Article>
            {
                List = articles,
                PageIndex = filterDto.PageIndex,
                PageSize = filterDto.PageSize,
                TotalPageCount = totalPagesCount
            };
        }

        public async Task<Domain.Article> GetByIdAsync(Guid id)
        {
            var result = await _ctx.Articles.Include(x => x.Category).Include(x => x.Comments)
                .Include(x => x.ArticleTags).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            var tags = await _ctx.ArticleTags.Include(x => x.Tag).Where(x => x.ArticleId == id).ToListAsync();

            result.ArticleTags = tags;
            result.Tags = tags.Select(x => x.Tag).ToList();
            return result;
        }

        public async Task<Domain.Article> UpdateAsync(Domain.Article entity)
        {
            _ctx.Articles.Update(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }


        public async Task<Domain.Article> AddTags(Domain.Article entity, List<Guid> tagIds)
        {
            foreach (var tagId in tagIds)
            {
                _ctx.ArticleTags.Add(new ArticleTag
                {
                    ArticleId = entity.Id,
                    TagId = tagId
                });
            }
            await _ctx.SaveChangesAsync();

            return await GetByIdAsync(entity.Id);
        }

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

        public async Task<Comment> GetCommentByIdAsync(Guid id)
        {
            var result = await _ctx.Comments.Include(c => c.Article).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            return result;
        }

        public async Task<Paged<Comment>> GetCommentsAsync(CommentFilterDto filterDto)
        {
            var query = _ctx.Comments.Include(x => x.Article).Where(x => !x.IsDeleted);

            if (filterDto.ArticleId != Guid.Empty)
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
