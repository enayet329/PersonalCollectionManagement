﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Data.Repositories;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/comments")]
    [Authorize(policy: "AdminOrUser")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("item/{id:guid}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetAllComments(Guid id)
        {
            var comments = await _commentService.GetAllCommentByItemIdAsync(id);
            return Ok(comments);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CommentDto>> GetCommentById(Guid id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentRequestDto comment)
        {
            var newComment = await _commentService.AddCommentAsync(comment);
            return Ok(newComment);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var result = await _commentService.DeleteCommentAsync(id);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateComment(CommentDto comment)
        {
            var result = await _commentService.UpdateCommentAsync(comment);
            return Ok(result);
        }
    }
}
