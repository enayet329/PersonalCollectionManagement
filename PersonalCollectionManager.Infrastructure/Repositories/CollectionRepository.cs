using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Infrastructure.Repositories
{
    public class CollectionRepository : Repository<Collection>, ICollectionRepository
    {
        public CollectionRepository(AppDbContext context, ILogger<Repository<Collection>> logger)
            : base(context, logger) { }

        public async Task<IEnumerable<Collection>> GetAllCollectionAsync()
        {
            return await _context.Set<Collection>()
                .Include(c => c.Items)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<Collection?> GetCollectionByIdAsync(Guid id)
        {
            return await _context.Set<Collection>()
                .Include(c => c.Items)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Collection?> GetCollectionByUserIdAsync(Guid userId)
        {
            return await _context.Set<Collection>()
                .Include(c => c.Items)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<IEnumerable<Collection>> GetCollectionsByUserIdAsync(Guid userId)
        {
            return await _context.Set<Collection>()
                .Include(c => c.Items)
                .Include(c => c.User)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Collection>> GetLargestCollectionsAsync(int count)
        {
            return await _context.Set<Collection>()
                .Include(c => c.Items)
                  .Include(c => c.User)
                .OrderByDescending(c => c.Items.Count)
                .Take(count)
                .ToListAsync();
        }
    }
}
