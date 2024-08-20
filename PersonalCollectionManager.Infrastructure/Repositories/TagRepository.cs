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
    }
}
