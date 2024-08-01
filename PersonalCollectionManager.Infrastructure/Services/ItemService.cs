

using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Infrastructure.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemService> _logger;
        public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task AddItemAsync(Item item)
        {
            try
            {
                await _itemRepository.AddAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding item");
                throw;
            }
        }

        public async Task DeleteItemAsync(Guid id)
        {
            try
            {
                var user = await _itemRepository.GetByIdAsync(id);
                _itemRepository.Remove(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting item");
                throw;
            }
        }

        public Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            try
            {
                return _itemRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all items");
                throw;
            }
        }

        public Task<Item> GetItemByIdAsync(Guid id)
        {
            try
            {
                return _itemRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting item by id");
                throw;
            }
        }

        public async Task UpdateItemAsync(Item item)
        {
            try
            {
                _itemRepository.Update(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Updating Item");
                throw;
            }
        }
    }
}
