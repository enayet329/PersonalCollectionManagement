using PersonalCollectionManager.Application.DTOs;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface ICollectionService 
    {
        Task<IEnumerable<CollectionDto>> GetAllCollectionsAsync();

        Task<IEnumerable<CollectionDto>> GetAllCollectionsByUserIdAsync(Guid id);
        Task<IEnumerable<CollectionDto>> GetLargestCollecitonAsync();
        Task<CollectionDto> GetCollectionByUserIdAsync(Guid id);

        Task<CollectionDto> GetCollectionByIdAsync(Guid id);
        Task<CollectionDto> AddCollectionAsync(CollectionRequestDto collection);
        Task<CollectionDto> UpdateCollectionAsync(CollectionDto collection);
        Task<OperationResult> DeleteCollectionAsync(Guid id);
    }
}
