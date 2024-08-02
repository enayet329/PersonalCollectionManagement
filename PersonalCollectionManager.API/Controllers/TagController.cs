
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Infrastructure.Services;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("get/Tags")]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetAllTagsAsync()
        {
            var tags = await _tagService.GetAllTagAsync();
            return Ok(tags);
        }

        [HttpGet("get/Tag/itemId")]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetTagByItemIdAsync(Guid id)
        {
            var tag = await _tagService.GetTagsByItemIdAsync(id);

            return Ok(tag);
        }

        [HttpGet("get/Tag/id")]
        public async Task<ActionResult<TagDto>> GetTagAsync(Guid id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            if (tag == null)
            {
                return NotFound("Tag not found");
            }
            return Ok(tag);
        }

        [HttpPost("add/Tag")]
        public async Task<ActionResult<TagDto>> AddTagAsync(TagRequestDto tag)
        {
            var newTag = await _tagService.AddTagAsync(tag);
            return Ok(newTag);
        }

        [HttpDelete("delete/Tag/id")]
        public async Task<ActionResult> DeleteTagAsync(Guid id)
        {
            var result = await _tagService.DeleteTagAsync(id);
            return Ok(result);
        }

        [HttpPut("update/Tag")]
        public async Task<ActionResult<TagDto>> UpdateTagAsync(TagDto tag)
        {
            var updatedTag = await _tagService.UpdateTagAsync(tag);
            return Ok(updatedTag);
        }


    }
}
