﻿using PersonalCollectionManager.Application.DTOs;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllCommentForItemAsync();
        Task<CommentDto> GetCommentByIdAsync(Guid id);
        Task<OperationResult> AddCommentAsync(CommentRequestDto comment);
        Task<OperationResult> UpdateCommentAsync(CommentRequestDto comment);
        Task<OperationResult> DeleteCommentAsync(Guid id);
    }
}
