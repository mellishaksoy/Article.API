using Article.API.Application.Dtos.Category;
using Article.API.Application.Services.Category;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Article.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
            {
                var application = await _categoryService.GetByIdAsync(id);
                return Ok(application);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<CategoryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllCategories(CategoryFilterDto filterDto)
        {
            try
            {
                var records = await _categoryService.GetAllAsync(filterDto);
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
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto model)
        {
            var result = await _categoryService.AddAsync(model);
            return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, null);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDto model)
        {
            var result = await _categoryService.UpdateAsync(model);
            return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, null);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var model = new CategoryDeleteDto()
            {
                Id = id
            };
            await _categoryService.DeleteAsync(model);
            return NoContent();
        }
    }
}

