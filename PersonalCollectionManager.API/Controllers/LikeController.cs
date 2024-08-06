using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/likes")]
    [Authorize(policy: "AdminOrUser")]
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

        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetLikesByItemIdAsync(Guid itemId)
        {
            var likes = await _like.GetAllLikeByItemId(itemId);
            return Ok(likes);
        }
    }
}
