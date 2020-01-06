using Article.API.Application.Dtos.Comment;
using Article.API.Application.Services.Comment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Article.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CommentsController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CommentDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            try
            {
                var res = await _commentService.GetByIdAsync(id);
                return Ok(res);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<CommentDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllCategories(CommentFilterDto filterDto)
        {
            try
            {
                var records = await _commentService.GetAllAsync(filterDto);
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
        public async Task<IActionResult> CreateComment([FromBody] CommentCreateDto model)
        {
            var result = await _commentService.AddAsync(model);
            return CreatedAtAction(nameof(GetCommentById), new { id = result.Id }, null);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateComment([FromBody] CommentUpdateDto model)
        {
            var result = await _commentService.UpdateAsync(model);
            return CreatedAtAction(nameof(GetCommentById), new { id = result.Id }, null);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var model = new CommentDeleteDto()
            {
                Id = id
            };
            await _commentService.DeleteAsync(model);
            return NoContent();
        }
    }
}
