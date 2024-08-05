
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IRepository
{
    public interface ICustomFieldRepository : IRepository<CustomField>
    {
        Task AddRangeAsync(IEnumerable<CustomField> customFields);
        Task<IEnumerable<CustomField>> GetByCollectionIdAsync(Guid collectionId);
    }
}
