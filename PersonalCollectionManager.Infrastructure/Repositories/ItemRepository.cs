using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Data;
using PersonalCollectionManager.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Data.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(AppDbContext context, ILogger<Repository<Item>> logger)
            : base(context, logger) { }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _context.Set<Item>()
                .Include(i => i.Collection)
                .ToListAsync();
        }

        public async Task<IEnumerable<ItemDto>> GetItemsByCollectionIdAsync(Guid id)
        {
            return await _context.Set<Item>()
                .Include(i => i.Collection)
                .Where(i => i.CollectionId == id)
                .Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    ImgUrl = i.ImgUrl,
                    Description = i.Description,
                    DateAdded = i.DateAdded,
                    CollectionId = i.CollectionId,
                    CollectionName = i.Collection.Name,
                    Likes = i.Likes.Count()
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<Item>> GetItemsByTagAsync(string tagName)
        {
            return await _context.Set<Tag>()
                .Where(t => t.Name == tagName)
                .SelectMany(t => t.ItemTags.Select(itemTag => itemTag.Item))
                .Include(i => i.Collection)
                .ToListAsync();
        }
        public async Task AddItemWithTagsAsync(Item item, List<Guid> tagIds)
        {
            foreach (var tagId in tagIds)
            {
                item.ItemTags.Add(new ItemTag { ItemId = item.Id, TagId = tagId });
            }

            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Item>> GetRecentItemsAsync()
        {
            return await _context.Set<Item>()
               .Include(i => i.Collection)
               .Include(i => i.ItemTags)
                   .ThenInclude(it => it.Tag)
               .OrderByDescending(i => i.DateAdded)
               .Take(10)
               .ToListAsync();
        }
    }
}
