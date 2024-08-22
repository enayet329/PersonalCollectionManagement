
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IRepository
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<IEnumerable<Tag>> GatAllTagsAsync();
        Task<IEnumerable<Tag>> GetByItemId(Guid id);
        Task<Tag> GetTagWithItemTagAsync(Guid tagId);
        Task<IEnumerable<Tag>> GetTopTagsAsync();
        Task<IEnumerable<Tag>> AddTagsAsync(IEnumerable<string> tagNames, Guid itemId);
        Task UpdateTagsForItemAsync(Guid itemId, IEnumerable<string> tagNames);
    }
}
