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

        public Task<OperationResult> AddCommentAsync(CommentRequestDto comment)
        {
            try
            {
                var entity = _mapper.Map<Comment>(comment);
                entity.CreatedAt = DateTime.Now;
                _commentRepository.AddAsync(entity);

                return Task.FromResult(new OperationResult(true, "Comment added successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding comment.");
                return Task.FromResult(new OperationResult(false, "Error adding comment."));
            }
        }

        public async Task<OperationResult> DeleteCommentAsync(Guid id)
        {
            try
            {
                var comment = await _commentRepository.GetByIdAsync(id);
                if(comment != null)
                {
                    _commentRepository.Remove(comment);
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

        public async Task<IEnumerable<CommentDTO>> GetAllCommentForItemAsync()
        {
            try
            {
                var comments = await _commentRepository.GetAllAsync();
                var dtos = _mapper.Map<IEnumerable<CommentDTO>>(comments);
                return dtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all comments for item.");
                return null;
            }
        }

        public Task<CommentDTO> GetCommentByIdAsync(Guid id)
        {
            try
            {
                var comment = _commentRepository.GetByIdAsync(id).Result;
                var dto = _mapper.Map<CommentDTO>(comment);
                return Task.FromResult(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting comment by id.");
                return null;
            }
        }

        public Task<OperationResult> UpdateCommentAsync(CommentRequestDto comment)
        {
            try
            {
                var entity = _mapper.Map<Comment>(comment);
                _commentRepository.Update(entity);

                return Task.FromResult(new OperationResult(true, "Comment updated successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating comment.");
                return Task.FromResult(new OperationResult(false, "Error updating comment."));
            }
        }
    }
}
