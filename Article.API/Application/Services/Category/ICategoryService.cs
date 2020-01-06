using Article.API.Application.Dtos.Category;
using Article.API.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Application.Services.Category
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetByIdAsync(Guid categoryId);
        Task<Paged<CategoryDto>> GetAllAsync(CategoryFilterDto filterDto);
        Task<CategoryDto> AddAsync(CategoryCreateDto dto);
        Task<CategoryDto> UpdateAsync(CategoryUpdateDto dto);
        Task DeleteAsync(CategoryDeleteDto dto);
    }
}
