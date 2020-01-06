using Article.API.Application.Dtos.Category;
using Article.API.Domain;
using Article.API.Domain.Common;
using Article.API.Infrastructure.Exceptions;
using Mapster;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Application.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryDto> AddAsync(CategoryCreateDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                throw new ArticleException(ArticleErrorCodes.CategoryNameCannotBeNull, "Category Name field is mandatory.", dto);
            }

            var entity = dto.Adapt<Domain.Category>();

            entity = await _categoryRepository.AddAsync(entity);

            return entity.Adapt<CategoryDto>();
        }

        public async Task DeleteAsync(CategoryDeleteDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.CategoryIdCannotBeNull, "Category Id field is mandatory.", dto);
            }

            var entity = await _categoryRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                throw new ArticleException(ArticleErrorCodes.CategoryCouldNotBeFound, "Category could not be found.", dto);
            }

            await _categoryRepository.DeleteAsync(entity);
        }

        public async Task<Paged<CategoryDto>> GetAllAsync(CategoryFilterDto filterDto)
        {
            var result = await _categoryRepository.GetCategoriesAsync(filterDto);

            var list = result.List.Select(x => x.Adapt<CategoryDto>()).ToList();
            var returnDto = new Paged<CategoryDto>
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

        public async Task<CategoryDto> GetByIdAsync(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.CategoryIdCannotBeNull, "Category Id field is mandatory.", null);
            }

            var entity = await _categoryRepository.GetByIdAsync(categoryId);

            if (entity == null)
            {
                throw new ArticleException(ArticleErrorCodes.CategoryCouldNotBeFound, "Category could not be found.", null);
            }

            return entity.Adapt<CategoryDto>();
        }

        public async Task<CategoryDto> UpdateAsync(CategoryUpdateDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                throw new ArticleException(ArticleErrorCodes.CategoryIdCannotBeNull, "Category Id field is mandatory.", dto);
            }

            var entity = await _categoryRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                throw new ArticleException(ArticleErrorCodes.CategoryCouldNotBeFound, "Category could not be found.", dto);
            }

            entity.Name = dto.Name;

            entity = await _categoryRepository.UpdateAsync(entity);

            return entity.Adapt<CategoryDto>();
        }
    }
}
