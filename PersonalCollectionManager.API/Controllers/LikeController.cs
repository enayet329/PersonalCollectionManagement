using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/likes")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _like;
        public LikeController(ILikeService like)
        {
            _like = like;
        }

        [HttpPost("toggle")]
        public async Task<IActionResult> ToggleLikeAsync(LikeRequestDto request)
        {
           var result = await _like.ToggleLike(request);
            return Ok(result);
        }

        [HttpGet("{itemId:guid}")]
        public async Task<IActionResult> GetLikesByItemIdAsync(Guid id)
        {
            var likes = await _like.GetAllLikeByItemId(id);
            return Ok(likes);
        }
    }
}
