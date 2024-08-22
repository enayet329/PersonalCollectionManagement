using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context, ILogger<Repository<Tag>> logger)
            : base(context, logger) { }

        public async Task<IEnumerable<Tag>> AddTagsAsync(IEnumerable<string> tagNames, Guid itemId)
        {
            var existingTags = await _context.Set<Tag>()
                .Where(t => tagNames.Contains(t.Name.ToLower()))
                .ToListAsync();

            var newTagNames = tagNames.Except(existingTags.Select(t => t.Name.ToLower())).ToList();
            var newTags = newTagNames.Select(name => new Tag { Name = name }).ToList();

            if (newTags.Any())
            {
                await _context.Set<Tag>().AddRangeAsync(newTags);
                await _context.SaveChangesAsync();
            }

            var allTags = existingTags.Concat(newTags).ToList();

            var itemTags = allTags.Select(tag => new ItemTag
            {
                ItemId = itemId,
                TagId = tag.Id
            });

            await _context.Set<ItemTag>().AddRangeAsync(itemTags);
            await _context.SaveChangesAsync();

            return allTags;
        }


        public async Task<IEnumerable<Tag>> GatAllTagsAsync()
        {
            try
            {
                return await _context.Set<Tag>()
                    .GroupBy(t => t.Name)
                    .Select(g => g.First())
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting all tags.");
                throw;
            }
        }

        public async Task<IEnumerable<Tag>> GetByItemId(Guid id)
        {
            try
            {
                return await _context.Set<ItemTag>()
                            .Where(it => it.ItemId == id)
                            .Select(it => it.Tag)
                            .ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting tags by item id.");
                throw;
            }
        }

        public async Task<Tag?> GetTagWithItemTagAsync(Guid tagId)
        {
            try
            {
                return await _context.Set<Tag>()
                        .SingleOrDefaultAsync(t => t.Id == tagId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tag with item tag.");
                throw;
            }
        }

        public async Task<IEnumerable<Tag>> GetTopTagsAsync()
        {
            try
            {
                return await _context.Set<Tag>()
                            .OrderByDescending(t => t.ItemTags.Count)
                            .Take(10)
                            .ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting top tags.");
                throw;
            }
        }

        public async Task UpdateTagsForItemAsync(Guid itemId, IEnumerable<string> tagNames)
        {
            var currentItemTags = await _context.Set<ItemTag>()
                .Include(it => it.Tag) 
                .Where(it => it.ItemId == itemId)
                .ToListAsync();

            var tagsToRemove = currentItemTags
                .Where(it => it.Tag != null && it.Tag.Name != null && !tagNames.Contains(it.Tag.Name.ToLower()))
                .ToList();

            if (tagsToRemove.Any())
            {
                _context.Set<ItemTag>().RemoveRange(tagsToRemove);
            }

            var existingTags = await _context.Set<Tag>()
                .Where(t => tagNames.Contains(t.Name.ToLower()))
                .ToListAsync();

            var newTagNames = tagNames.Except(existingTags.Select(t => t.Name.ToLower())).ToList();
            var newTags = newTagNames.Select(name => new Tag { Name = name }).ToList();

            if (newTags.Any())
            {
                await _context.Set<Tag>().AddRangeAsync(newTags);
                await _context.SaveChangesAsync();
            }

            var allTags = existingTags.Concat(newTags).ToList();

            var newItemTags = allTags
                .Where(t => !currentItemTags.Any(it => it.TagId == t.Id))
                .Select(t => new ItemTag { ItemId = itemId, TagId = t.Id });

            await _context.Set<ItemTag>().AddRangeAsync(newItemTags);
            await _context.SaveChangesAsync();
        }

    }
}
