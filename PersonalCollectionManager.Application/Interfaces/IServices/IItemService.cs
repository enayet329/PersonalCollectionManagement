using PersonalCollectionManager.Application.DTOs;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface IItemService 
    {
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();
        Task<IEnumerable<ItemDto>> GetAllItemByCollectionIdAsync(Guid id);
        Task<IEnumerable<ItemDto>> GetRecentItemsAsync();
        Task<IEnumerable<ItemDto>> GetItemsByTagAsync(string tag);
        Task<ItemDto> GetItemByIdAsync(Guid id);
        Task<ItemDto> AddItemAsync(ItemRequestDto item);
        Task<ItemDto> UpdateItemAsync(ItemDto item);
        Task<OperationResult> DeleteItemAsync(Guid id);
    }
}
