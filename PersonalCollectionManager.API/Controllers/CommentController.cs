using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/comments")]
    [ApiController]
    [Authorize(Policy = "AdminOrUser")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("itemId")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetAllCommentsByItemId(Guid itemId)
        {
            var comments = await _commentService.GetAllCommentByItemIdAsync(itemId);
            return Ok(comments);
        }

        [HttpGet("get/Id")]
        public async Task<ActionResult<CommentDto>> GetCommentById(Guid commentId)
        {
            var comment = await _commentService.GetCommentByIdAsync(commentId);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentRequestDto commentRequest)
        {
            var newComment = await _commentService.AddCommentAsync(commentRequest);
            return Ok(newComment);
        }

        [HttpDelete("delete/commentId")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            var result = await _commentService.DeleteCommentAsync(commentId);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateComment(Guid commentId, [FromBody] CommentDto commentUpdate)
        {
            commentUpdate.Id = commentId;
            var result = await _commentService.UpdateCommentAsync(commentUpdate);
            return Ok(result);
        }
    }
}
