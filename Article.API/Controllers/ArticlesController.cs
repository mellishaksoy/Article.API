using Article.API.Application.Dtos.Article;
using Article.API.Application.Services.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Article.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ArticlesController : BaseController
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ArticleDto), (int)HttpStatusCode.OK)]
        [Authorize]
        public async Task<IActionResult> GetArticleById(Guid id)
        {
            try
            {
                var application = await _articleService.GetByIdAsync(id);
                return Ok(application);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<ArticleDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetAllArticles(ArticleFilterDto filterDto)
        {
            try
            {
                var records = await _articleService.GetAllAsync(filterDto);
                return PagedOk(records, filterDto, records.TotalPageCount);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        public async Task<IActionResult> CreateArticle([FromBody] ArticleCreateDto model)
        {
            var result = await _articleService.AddAsync(model);
            return CreatedAtAction(nameof(GetArticleById), new { id = result.Id }, null);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Authorize]
        public async Task<IActionResult> UpdateArticle([FromBody] ArticleUpdateDto model)
        {
            var result = await _articleService.UpdateAsync(model);
            return CreatedAtAction(nameof(GetArticleById), new { id = result.Id }, null);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [Authorize]
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            var model = new ArticleDeleteDto()
            {
                Id = id
            };
            await _articleService.DeleteAsync(model);
            return NoContent();
        }
    }
}
