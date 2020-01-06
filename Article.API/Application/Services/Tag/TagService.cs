using Article.API.Application.Dtos.Tag;
using Article.API.Domain;
using Article.API.Domain.Common;
using Article.API.Infrastructure.Exceptions;
using Mapster;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Application.Services.Tag
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<TagDto> AddAsync(TagCreateDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                throw new ArticleException(ArticleErrorCodes.TagNameCannotBeNull, "Tag Name field is mandatory.", dto);
            }

            var entity = dto.Adapt<Domain.Tag>();

            entity = await _tagRepository.AddAsync(entity);

            return entity.Adapt<TagDto>();
        }

        public async Task DeleteAsync(TagDeleteDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.TagIdCannotBeNull, "Tag Id field is mandatory.", dto);
            }

            var entity = await _tagRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                throw new ArticleException(ArticleErrorCodes.TagCouldNotBeFound, "Tag could not be found.", dto);
            }

            if (entity.ArticleTags != null && entity.ArticleTags.Any())
            {
                throw new ArticleException(ArticleErrorCodes.TagCannotBeDelete, "Tag Cannot Be Deleted because an article is related.", dto);
            }

            await _tagRepository.DeleteAsync(entity);
        }

        public async Task<Paged<TagDto>> GetAllAsync(TagFilterDto tagFilterDto)
        {
            var result = await _tagRepository.GetTagsAsync(tagFilterDto);

            var list = result.List.Select(x => x.Adapt<TagDto>()).ToList();
            var returnDto = new Paged<TagDto>
            {
                List = list,
                PageIndex = tagFilterDto.PageIndex,
                PageSize = tagFilterDto.PageSize,
                TotalPageCount = result.TotalPageCount,
                FilteredCount = result.FilteredCount,
                TotalCount = result.TotalCount
            };

            return returnDto;
        }

        public async Task<TagDto> GetByIdAsync(Guid tagId)
        {
            if (tagId == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.TagIdCannotBeNull, "Tag Id field is mandatory.", null);
            }

            var entity = await _tagRepository.GetByIdAsync(tagId);

            if (entity == null)
            {
                throw new ArticleException(ArticleErrorCodes.TagCouldNotBeFound, "Tag could not be found.", null);
            }

            return entity.Adapt<TagDto>();
        }

        public async Task<TagDto> UpdateAsync(TagUpdateDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.TagIdCannotBeNull, "Tag Id field is mandatory.", dto);
            }

            var entity = await _tagRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                throw new ArticleException(ArticleErrorCodes.TagCouldNotBeFound, "Tag could not be found.", dto);
            }

            entity.Name = dto.Name;

            entity = await _tagRepository.UpdateAsync(entity);

            return entity.Adapt<TagDto>();
        }
    }
}
