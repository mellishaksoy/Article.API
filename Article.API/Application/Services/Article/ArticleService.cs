using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Article.API.Application.Dtos.Article;
using Article.API.Application.Dtos.Tag;
using Article.API.Domain;
using Article.API.Domain.Common;
using Article.API.Infrastructure.Exceptions;
using Mapster;

namespace Article.API.Application.Services.Article
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        public ArticleService(IArticleRepository articleRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }
        public async Task<ArticleDto> AddAsync(ArticleCreateDto dto)
        {
            if (string.IsNullOrEmpty(dto.Title))
            {
                throw new ArticleException(ArticleErrorCodes.ArticleTitleCannotBeNull, "Article Title field is mandatory.", dto);
            }

            if (string.IsNullOrEmpty(dto.Body))
            {
                throw new ArticleException(ArticleErrorCodes.ArticleBodyCannotBeNull, "Article Body field is mandatory.", dto);
            }

            if (dto.CategoryId == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.ArticleCategoryCannotBeNull, "Article Category Id field is mandatory.", dto);
            }
            else
            {
                var categoryEntity = await _categoryRepository.GetByIdAsync(dto.CategoryId);
                if(categoryEntity == null || categoryEntity.Id == Guid.Empty)
                {
                    throw new ArticleException(ArticleErrorCodes.CategoryCouldNotBeFound, "Article Category Id could not be found..", dto);
                }
            }

            var entity = dto.Adapt<Domain.Article>();

            entity = await _articleRepository.AddAsync(entity);

            if (dto.TagIds != null && dto.TagIds.Any())
            {
                if(!await _tagRepository.CheckAllTagIdsExist(dto.TagIds))
                {
                    throw new ArticleException(ArticleErrorCodes.TagCouldNotBeFound, "Article cannot contain non-existed tagId", dto);
                }

                entity = await _articleRepository.AddTags(entity, dto.TagIds);
            }

            return entity.Adapt<ArticleDto>();
        }

        public async Task DeleteAsync(ArticleDeleteDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.ArticleIdCannotBeNull, "Article Id field is mandatory.", dto);
            }

            var entity = await _articleRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                throw new ArticleException(ArticleErrorCodes.ArticleCouldNotBeFound, "Article could not be found.", dto);
            }
            
            // to do delete comments


            await _articleRepository.DeleteAsync(entity);
        }

        public async Task<Paged<ArticleDto>> GetAllAsync(ArticleFilterDto filterDto)
        {
            var result = await _articleRepository.GetArticlesAsync(filterDto);

            var list = result.List.Select(x => x.Adapt<ArticleDto>()).ToList();
            var returnDto = new Paged<ArticleDto>
            {
                List = list,
                PageIndex = filterDto.PageIndex,
                PageSize = filterDto.PageSize,
                TotalPageCount = result.TotalPageCount,
                FilteredCount = result.FilteredCount,
                TotalCount = result.TotalCount
            };

            return returnDto;
        }

        public async Task<ArticleDto> GetByIdAsync(Guid articleId)
        {
            if (articleId == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.ArticleIdCannotBeNull, "Article Id field is mandatory.", null);
            }

            var entity = await _articleRepository.GetByIdAsync(articleId);

            if (entity == null)
            {
                throw new ArticleException(ArticleErrorCodes.ArticleCouldNotBeFound, "Article could not be found.", null);
            }
            
            var articleDto = entity.Adapt<ArticleDto>();
            
            
            return articleDto;
        }

        public async Task<ArticleDto> UpdateAsync(ArticleUpdateDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.ArticleIdCannotBeNull, "Article Id field is mandatory.", dto);
            }

            var entity = await _articleRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                throw new ArticleException(ArticleErrorCodes.ArticleCouldNotBeFound, "Article could not be found.", dto);
            }

            entity.Title = dto.Title;
            entity.Body = dto.Body;
            entity.CategoryId = dto.CategoryId;

            entity = await _articleRepository.UpdateAsync(entity);

            // article-tag update
            //var existTagIds = entity.Tags.Select(x => x.Id).ToList();
            //var insertedTagIds = dto.TagIds.Except(existTagIds).ToList();
            //var deletedTagIds = existTagIds.Except(dto.TagIds).ToList();

            //foreach(var t in insertedTagIds)
            //{
            //    // insert article-tag
            //}

            //foreach(var t in deletedTagIds)
            //{
            //    // delete article-tags
            //}

            return entity.Adapt<ArticleDto>();
        }
    }
}
