
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IRepository
{
    public interface ICollectionRepository : IRepository<Collection>
    {
        Task<IEnumerable<Collection>> GetAllCollectionAsync();
        Task<Collection> GetCollectionByIdAsync(Guid id);
        Task<IEnumerable<Collection>> GetCollectionsByUserIdAsync(Guid userId);
        Task<Collection> GetCollectionByUserIdAsync(Guid userId);
        Task<IEnumerable<Collection>> GetLargestCollectionsAsync(int count);
    }
}
