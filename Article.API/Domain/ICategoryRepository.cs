using Article.API.Application.Dtos.Category;
using Article.API.Domain.Common;
using System;
using System.Threading.Tasks;

namespace Article.API.Domain
{
    public interface ICategoryRepository : IRepository
    {
        Task<Category> AddAsync(Category entity);
        Task<Category> GetByIdAsync(Guid id);
        Task DeleteAsync(Category entity);
        Task<Category> UpdateAsync(Category entity);
        Task<Paged<Category>> GetCategoriesAsync(CategoryFilterDto filterDto);
    }
}
