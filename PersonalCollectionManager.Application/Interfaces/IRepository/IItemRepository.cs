using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Application.Interfaces.IRepository
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<AlgoliaItemDto?> GetItemByIdAsync(Guid id);
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<IEnumerable<ItemDto>> GetItemsByCollectionIdAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsByTagAsync(string tagName);
        Task<IEnumerable<Item>> GetRecentItemsAsync();
        Task<ItemDto> GetItemsById(Guid itemId);
        Task<bool> DeleteItemAsync(Guid itemId);
    }
}
