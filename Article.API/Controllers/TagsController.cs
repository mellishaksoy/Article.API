using Article.API.Application.Dtos.Tag;
using Article.API.Application.Services.Tag;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Article.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class TagsController : BaseController
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(TagDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTagById(Guid id)
        {
            try
            {
                var application = await _tagService.GetByIdAsync(id);
                return Ok(application);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<TagDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllTags(TagFilterDto filterDto)
        {
            try
            {
                var records = await _tagService.GetAllAsync(filterDto);
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
        public async Task<IActionResult> CreateTag([FromBody] TagCreateDto model)
        {
            var result = await _tagService.AddAsync(model);
            return CreatedAtAction(nameof(GetTagById), new { id = result.Id }, null);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateTag([FromBody] TagUpdateDto model)
        {
            var result = await _tagService.UpdateAsync(model);
            return CreatedAtAction(nameof(GetTagById), new { id = result.Id }, null);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var model = new TagDeleteDto()
            {
                Id = id
            };
            await _tagService.DeleteAsync(model);
            return NoContent();
        }
    }
}
