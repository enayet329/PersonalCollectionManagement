

using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.DTOs;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Data.Repositories;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Infrastructure.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemTagRepository _itemTagRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemService> _logger;
        public ItemService(IItemRepository itemRepository, IItemTagRepository itemTagRepository, IMapper mapper, ILogger<ItemService> logger)
        {
            _itemRepository = itemRepository;
            _itemTagRepository = itemTagRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ItemDto> AddItemAsync(ItemRequestDto item)
        {
            try
            {
                var items = _mapper.Map<Item>(item);
                items.DateAdded = DateTime.Now;
                await _itemRepository.AddAsync(items);

                var result = await _itemRepository.GetItemsById(items.Id);
                return _mapper.Map<ItemDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding item");
                return null;
            }
        }

        public async Task<OperationResult> DeleteItemAsync(Guid id)
        {
            try
            {
                var item = await _itemRepository.DeleteItemAsync(id);
                if(item == false)
                {
                    return new OperationResult(false, "Error deleting item");
                }
                return new OperationResult(true, "Item deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting item");
                return new OperationResult(false, "Error deleting item");
            }
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemByCollectionIdAsync(Guid id)
        {
            try
            {
                var item = await _itemRepository.GetItemsByCollectionIdAsync(id);
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all items by collection id");
                throw;
            }
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
        {
            try
            {
                var user = await _itemRepository.GetAllItemsAsync();
                return _mapper.Map<IEnumerable<ItemDto>>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all items");
                throw;
            }
        }

        public async Task<ItemDto> GetItemByIdAsync(Guid id)
        {
            try
            {
                var user = await _itemRepository.GetItemsById(id);
                return _mapper.Map<ItemDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting item by id");
                throw;
            }
        }

        public async Task<IEnumerable<ItemDto>> GetItemsByTagAsync(string tagName)
        {
            try
            {
                var items = await _itemRepository.GetItemsByTagAsync(tagName);
                return _mapper.Map<IEnumerable<ItemDto>>(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting items by tag");
                throw;
            }
        }

        public async Task<IEnumerable<ItemDto>> GetRecentItemsAsync()
        {
            try
            {
                var item = await _itemRepository.GetRecentItemsAsync();

                return _mapper.Map<IEnumerable<ItemDto>>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting recent items");
                throw;
            }
        }

        public async Task<ItemDto> UpdateItemAsync(ItemUpdateRequestDto item)
        {
            try
            {
                var items = _mapper.Map<Item>(item);
                await _itemRepository.Update(items);
                
                var result = await _itemRepository.GetItemsById(items.Id);
                return _mapper.Map<ItemDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Updating Item");
                return null;
            }
        }

    }
}
