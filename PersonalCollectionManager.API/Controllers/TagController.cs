﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/tags")]
    [ApiController]
    [Authorize(Policy = "AdminOrUser")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetAllTagsAsync()
        {
            var tags = await _tagService.GetAllTagAsync();
            return Ok(tags);
        }

        [HttpGet("top")]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetPopularTagsAsync()
        {
            var tags = await _tagService.GetPopularTagsAsync();
            if (tags == null || !tags.Any())
            {
                return NotFound(new { message = "No popular tags found" });
            }
            return Ok(tags);
        }

        [HttpGet("itemId")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetTagsByItemIdAsync(Guid itemId)
        {
            var tags = await _tagService.GetTagsByItemIdAsync(itemId);
            if (tags == null || !tags.Any())
            {
                return NotFound(new { message = "No tags found for this item." });
            }
            return Ok(tags);
        }

        [HttpGet("id")]
        public async Task<ActionResult<TagDto>> GetTagByIdAsync(Guid id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            if (tag == null)
            {
                return NotFound(new { message = "Tag not found." });
            }
            return Ok(tag);
        }

        [HttpPost("add")]
        public async Task<ActionResult<TagDto>> AddTagAsync([FromBody] IEnumerable<TagRequestDto> tagRequest)
        {
            var newTag = await _tagService.AddTagAsync(tagRequest);
            return Ok(newTag);
        }

        [HttpDelete("delete/id")]
        public async Task<IActionResult> DeleteTagAsync(Guid id)
        {
            var result = await _tagService.DeleteTagAsync(id);
            if (result == null)
            {
                return NotFound(new { message = "Tag not found." });
            }
            return NoContent();
        }

        [HttpPut("update/{itemId}/tag")]
        public async Task<IActionResult> UpdateTagAsync([FromRoute] Guid itemId, [FromBody] IEnumerable<TagDto> tagDto)
        {

            var updatedTag = await _tagService.UpdateTagAsync(itemId,tagDto);

            return Ok(updatedTag);
        }
    }
}
