using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Data.Repositories;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("get/comments")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetAllComments()
        {
            var comments = await _commentService.GetAllCommentForItemAsync();
            return Ok(comments);
        }

        [HttpGet("get/comment/id")]
        public async Task<ActionResult<CommentDto>> GetCommentById(Guid id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            return Ok(comment);
        }

        [HttpPost("add/comment")]
        public async Task<IActionResult> AddComment(CommentRequestDto comment)
        {
            var newComment = await _commentService.AddCommentAsync(comment);
            return Ok(newComment);
        }

        [HttpDelete("delete/comment/id")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var result = await _commentService.DeleteCommentAsync(id);
            return Ok(result);
        }

        [HttpPut("update/comment")]
        public async Task<IActionResult> UpdateComment(CommentRequestDto comment)
        {
            var result = await _commentService.UpdateCommentAsync(comment);
            return Ok(result);
        }
    }
}
