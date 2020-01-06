using Article.API.Application.Dtos.Article;
using Article.API.Domain.Common;
using System;
using System.Threading.Tasks;

namespace Article.API.Application.Services.Article
{
    public interface IArticleService
    {
        Task<ArticleDto> GetByIdAsync(Guid articleId);
        Task<Paged<ArticleDto>> GetAllAsync(ArticleFilterDto filterDto);
        Task<ArticleDto> AddAsync(ArticleCreateDto dto);
        Task<ArticleDto> UpdateAsync(ArticleUpdateDto dto);
        Task DeleteAsync(ArticleDeleteDto dto);
    }
}
