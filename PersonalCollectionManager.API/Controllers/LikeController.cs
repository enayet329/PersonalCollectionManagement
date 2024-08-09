using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using System;
using System.Threading.Tasks;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/likes")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("toggle")]
        public async Task<IActionResult> ToggleLikeAsync([FromBody] LikeRequestDto request)
        {
            var result = await _likeService.ToggleLike(request);
            return Ok(result);
        }

        [HttpGet("itemId")]
        public async Task<IActionResult> GetLikesByItemIdAsync(Guid itemId)
            {
            var likes = await _likeService.GetAllLikeByItemId(itemId);
            return Ok(likes);
        }
    }
}
