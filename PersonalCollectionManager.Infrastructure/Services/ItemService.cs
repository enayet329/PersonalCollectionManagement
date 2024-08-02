﻿

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
        private readonly IMapper _mapper;
        private readonly ILogger<ItemService> _logger;
        public ItemService(IItemRepository itemRepository,IMapper mapper, ILogger<ItemService> logger)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ItemDto> AddItemAsync(ItemRequestDto item)
        {
            try
            {
                var user = _mapper.Map<Item>(item);
                var result = await _itemRepository.AddAsync(user);
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
                var user = await _itemRepository.GetByIdAsync(id);
                await _itemRepository.Remove(user);
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
                var user = await _itemRepository.GetItemsByCollectionIdAsync(id);
                return _mapper.Map<IEnumerable<ItemDto>>(user);
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
                var user = await _itemRepository.GetByIdAsync(id);
                return _mapper.Map<ItemDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting item by id");
                throw;
            }
        }

        public async Task<IEnumerable<ItemDto>> GetRecentItemsAsync()
        {
            try
            {
                var item =await _itemRepository.GetRecentItemsAsync();

                return _mapper.Map<IEnumerable<ItemDto>>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting recent items");
                throw;
            }
        }

        public async Task<ItemDto> UpdateItemAsync(ItemDto item)
        {
            try
            {
                var user = _mapper.Map<Item>(item);
                var result = await _itemRepository.Update(user);
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
