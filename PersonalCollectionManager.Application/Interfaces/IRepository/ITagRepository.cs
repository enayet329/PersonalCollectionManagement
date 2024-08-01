
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IRepository
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<Tag> GetTagByNameAsync(string name);

    }
}
