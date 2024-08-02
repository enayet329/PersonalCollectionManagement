﻿using PersonalCollectionManager.Application.DTOs;
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
        Task<ItemDto> GetItemByIdAsync(Guid id);
        Task<OperationResult> AddItemAsync(ItemRequestDto item);
        Task<OperationResult> UpdateItemAsync(ItemDto item);
        Task<OperationResult> DeleteItemAsync(Guid id);
    }
}
