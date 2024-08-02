using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _like;
        public LikeController(ILikeService like)
        {
            _like = like;
        }

        [HttpPost("tagle/like")]
        public async Task<IActionResult> TagleLikeAsync(LikeRequestDto request)
        {
           var result = await _like.ToggleLike(request);
            return Ok(result);
        }

        [HttpPost("get/likes")]
        public async Task<IActionResult> GetLikesByItemIdAsync(Guid id)
        {
            var likes = await _like.GetAllLikeByItemId(id);
            return Ok(likes);
        }
    }
}
