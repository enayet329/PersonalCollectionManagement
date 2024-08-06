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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("item/{itemId:guid}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetAllCommentsByItemId([FromRoute] Guid itemId)
        {
            var comments = await _commentService.GetAllCommentByItemIdAsync(itemId);
            return Ok(comments);
        }

        [HttpGet("{commentId:guid}")]
        public async Task<ActionResult<CommentDto>> GetCommentById([FromRoute] Guid commentId)
        {
            var comment = await _commentService.GetCommentByIdAsync(commentId);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CommentRequestDto commentRequest)
        {
            var newComment = await _commentService.AddCommentAsync(commentRequest);
            return Ok(newComment);
        }

        [HttpDelete("{commentId:guid}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid commentId)
        {
            var result = await _commentService.DeleteCommentAsync(commentId);
            return Ok(result);
        }

        [HttpPut("{commentId:guid}")]
        public async Task<IActionResult> UpdateComment([FromRoute] Guid commentId, [FromBody] CommentDto commentUpdate)
        {
            commentUpdate.Id = commentId;
            var result = await _commentService.UpdateCommentAsync(commentUpdate);
            return Ok(result);
        }
    }
}
