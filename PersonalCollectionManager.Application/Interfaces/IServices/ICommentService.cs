using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllCommentForItemAsync();
        Task<Comment> GetCommentByIdAsync(Guid id);
        Task AddCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(Guid id);
    }
}
