using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger _logger;
        public CommentService(ICommentRepository commentRepository, ILogger<CommentService> logger)
        {
            _commentRepository = commentRepository;
            _logger = logger;
        }

        public Task AddCommentAsync(Comment comment)
        {
            try
            {
                _commentRepository.AddAsync(comment);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Add Comment");
                throw;
            }

        }

        public async Task DeleteCommentAsync(Guid id)
        {
            try
            {
                var user = await _commentRepository.GetByIdAsync(id);
                _commentRepository.Remove(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Delete Comment");
                throw;
            }

        }

        public async Task<IEnumerable<Comment>> GetAllCommentForItemAsync()
        {
            try
            {
                return await _commentRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Get All Comment");
                throw;
            }
        }

        public async Task<Comment> GetCommentByIdAsync(Guid id)
        {
            try
            {
                return await _commentRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Get Comment by id");
                throw;
            }
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            try
            {
                _commentRepository.Update(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Update Comment");
                throw;
            }
        }
    }
}
