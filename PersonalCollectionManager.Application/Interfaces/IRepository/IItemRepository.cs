﻿using PersonalCollectionManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Application.Interfaces.IRepository
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<IEnumerable<Item>> GetItemsByCollectionIdAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsByTagAsync(string tagName);
        Task<IEnumerable<Item>> GetRecentItemsAsync();
        Task AddItemWithTagsAsync(Item item, List<Guid> tagIds);
    }
}
