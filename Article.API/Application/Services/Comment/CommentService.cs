using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Article.API.Application.Dtos.Comment;
using Article.API.Domain;
using Article.API.Domain.Common;
using Article.API.Infrastructure.Exceptions;
using Mapster;

namespace Article.API.Application.Services.Comment
{
    public class CommentService : ICommentService
    {
        private readonly IArticleRepository _articleRepository;
        public CommentService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<CommentDto> AddAsync(CommentCreateDto dto)
        {
            if (string.IsNullOrEmpty(dto.Body))
            {
                throw new ArticleException(ArticleErrorCodes.CommentBodyCannotBeNull, "Comment Body field is mandatory.", dto);
            }

            if (dto.ArticleId == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.CommentArticleIdConnotBeNull, "Comment Article Id field is mandatory.", dto);
            }

            var articleEntity = _articleRepository.GetByIdAsync(dto.ArticleId);
            if(articleEntity == null)
            {
                throw new ArticleException(ArticleErrorCodes.ArticleCouldNotBeFound, "Article could not be found.", null);
            }
            var entity = dto.Adapt<Domain.Comment>();

            entity = await _articleRepository.AddAsync(entity);

            return entity.Adapt<CommentDto>();
        }

        public async Task DeleteAsync(CommentDeleteDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.CommentIdCannotBeNull, "Comment Id field is mandatory.", dto);
            }

            var entity = await _articleRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                throw new ArticleException(ArticleErrorCodes.CommentCouldNotBeFound, "Comment could not be found.", dto);
            }

            await _articleRepository.DeleteAsync(entity);
        }

        public async Task<Paged<CommentDto>> GetAllAsync(CommentFilterDto filterDto)
        {
            var result = await _articleRepository.GetCommentsAsync(filterDto);

            var list = result.List.Select(x => x.Adapt<CommentDto>()).ToList();
            var returnDto = new Paged<CommentDto>
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

        public async Task<CommentDto> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.CommentIdCannotBeNull, "Comment Id field is mandatory.", null);
            }

            var entity = await _articleRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new ArticleException(ArticleErrorCodes.CommentCouldNotBeFound, "Comment could not be found.", null);
            }

            return entity.Adapt<CommentDto>();
        }

        public async Task<CommentDto> UpdateAsync(CommentUpdateDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.CommentIdCannotBeNull, "Comment Id field is mandatory.", null);
            }

            var entity = await _articleRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                throw new ArticleException(ArticleErrorCodes.CommentCouldNotBeFound, "Comment could not be found.", dto);
            }

            if (string.IsNullOrEmpty(dto.Body))
            {
                throw new ArticleException(ArticleErrorCodes.CommentBodyCannotBeNull, "Comment Body field is mandatory.", dto);
            }

            entity.Body = dto.Body;

            entity = await _articleRepository.UpdateAsync(entity);

            return entity.Adapt<CommentDto>();
        }
    }
}
