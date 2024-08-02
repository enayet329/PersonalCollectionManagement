using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CommentService(ICommentRepository commentRepository,IMapper mapper, ILogger<CommentService> logger)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult> AddCommentAsync(CommentRequestDto comment)
        {
            try
            {
                var entity = _mapper.Map<Comment>(comment);
                entity.CreatedAt = DateTime.Now;
                await _commentRepository.AddAsync(entity);

                return new OperationResult(true, "Comment added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding comment.");
                return new OperationResult(false, "Error adding comment.");
            }
        }

        public async Task<OperationResult> DeleteCommentAsync(Guid id)
        {
            try
            {
                var comment = await _commentRepository.GetByIdAsync(id);
                if(comment != null)
                {
                    await _commentRepository.Remove(comment);
                    return new OperationResult(true, "Comment deleted successfully.");
                }
                    return new OperationResult(false, "Comment not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting comment.");
                return new OperationResult(false, "Error deleting comment.");
            }
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentByItemIdAsync(Guid id)
        {
            try
            {
                var comments = await _commentRepository.GetCommentsByItemAsync(id);
                var dtos = _mapper.Map<IEnumerable<CommentDto>>(comments);
                return dtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all comments for item.");
                return null;
            }
        }

        public async Task<CommentDto> GetCommentByIdAsync(Guid id)
        {
            try
            {
                var comment = await _commentRepository.GetByIdAsync(id);
                var dto = _mapper.Map<CommentDto>(comment);
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting comment by id.");
                return null;
            }
        }

        public async Task<OperationResult> UpdateCommentAsync(CommentDto comment)
        {
            try
            {
                var entity = _mapper.Map<Comment>(comment);
                await _commentRepository.Update(entity);

                return new OperationResult(true, "Comment updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating comment.");
                return new OperationResult(false, "Error updating comment.");
            }
        }
    }
}
