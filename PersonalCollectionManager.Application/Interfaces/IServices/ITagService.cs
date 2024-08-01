using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface ITagService 
    {
        Task<IEnumerable<Tag>> GetAllTagAsync();
        Task<Tag> GetTagByIdAsync(Guid id);
        Task AddTagAsync(Tag tag);
        Task UpdateTagAsync(Tag tag);
        Task DeleteTagAsync(Guid id);
    }
}
