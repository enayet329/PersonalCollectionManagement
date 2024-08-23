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
                .Include(i => i.ItemTags)
                .ThenInclude(i => i.Tag)
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

        public async Task<ItemDto?> GetItemsById(Guid itemId)
        {
            return await _context.Set<Item>()
                .Include(i => i.Collection)
                .Include(i => i.ItemTags)
                    .ThenInclude(it => it.Tag)
                .Where(i => i.Id == itemId)
                .Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    ImgUrl = i.ImgUrl,
                    Description = i.Description,
                    DateAdded = i.DateAdded,
                    CollectionId = i.CollectionId,
                    CollectionName = i.Collection.Name,
                    Likes = i.Likes.Count(),
                    TagNames = i.ItemTags
                            .Select(it => it.Tag.Name)
                            .FirstOrDefault() == null
                             ? new List<string>()
                             : new List<string> { i.ItemTags.Select(it => it.Tag.Name).FirstOrDefault() },
                   userId = i.Collection.UserId
                }

                 )
                .SingleOrDefaultAsync();
        }

        public async Task<AlgoliaItemDto?> GetItemByIdAsync(Guid id)
        {
            return await _context.Set<Item>()
                .Include(i => i.Collection)
                .Include(i => i.ItemTags)
                    .ThenInclude(it => it.Tag)
                .Include(i => i.Comments)
                .Include(i => i.Likes)
                .Include(i => i.CustomFieldValues)
                    .ThenInclude(cf => cf.CustomField)
                .Where(i => i.Id == id)
                .Select(i => new AlgoliaItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    ImgUrl = i.ImgUrl,
                    Description = i.Description,
                    DateAdded = i.DateAdded,
                    CollectionId = i.CollectionId,
                    CollectionName = i.Collection.Name,
                    CotegorieName = i.Collection.Topic,
                    CollectionDescription = i.Collection.Description,
                    TagNames = i.ItemTags.Select(it => it.Tag.Name).ToList(),
                    CustomFieldValues = i.CustomFieldValues.Select(cf => cf.Value).ToList(),
                    Likes = i.Likes.Count,
                    Comments = i.Comments.Select(c => c.Content).ToList()
                })
                .SingleOrDefaultAsync();
        }


        public async Task<bool> DeleteItemAsync(Guid itemId)
        {
            try
            {
                var item = await _context.Items
                    .Include(i => i.CustomFieldValues)
                    .SingleOrDefaultAsync(i => i.Id == itemId);

                if (item == null)
                {
                    return false;
                }

                if (item.CustomFieldValues != null && item.CustomFieldValues.Any())
                {
                    _context.CustomFieldValues.RemoveRange(item.CustomFieldValues);
                }

                _context.Items.Remove(item);


                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting item with ID: {itemId}");
                return false;
            }
        }

    }
}

