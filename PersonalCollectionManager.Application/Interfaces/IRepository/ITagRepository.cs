
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IRepository
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<IEnumerable<Tag>> GetByItemId(Guid id);

    }
}
