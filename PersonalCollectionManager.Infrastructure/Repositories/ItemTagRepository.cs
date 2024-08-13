using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Infrastructure.Repositories
{
    public class ItemTagRepository : Repository<ItemTag>, IItemTagRepository
    {
        public ItemTagRepository(AppDbContext context, ILogger<Repository<ItemTag>> logger) : base(context, logger)
        {
        }

        public void AddTagToItem(Guid itemId, Guid tagId)
        {
            var itemTag = new ItemTag
            {
                ItemId = itemId,
                TagId = tagId
            };

            _context.ItemTags.Add(itemTag);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<ItemTag>> getTagsByItemAsync(Guid itemId)
        {
            return await _context.ItemTags.Where(it => it.ItemId == itemId).ToListAsync();
        }

        public void RemoveTagFromItem(Guid itemId, Guid tagId)
        {
            var itemTag = _context.ItemTags
                .FirstOrDefault(it => it.ItemId == itemId && it.TagId == tagId);

            if (itemTag != null)
            {
                _context.ItemTags.Remove(itemTag);
                _context.SaveChanges();
            }
        }

    }
}
