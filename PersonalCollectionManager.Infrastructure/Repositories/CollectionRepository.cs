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
            return await _context.Set<Collection>().ToListAsync();
        }

        public async Task<Collection?> GetCollectionByIdAsync(Guid id)
        {
            return await _context.Set<Collection>().FindAsync(id);
        }

        public async Task<Collection?> GetCollectionByUserIdAsync(Guid userId)
        {
            return await _context.Set<Collection>().FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<IEnumerable<Collection>> GetCollectionsByUserIdAsync(Guid userId)
        {
            return await _context.Set<Collection>().Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Collection>> GetLargestCollectionsAsync(int count)
        {
            return await _context.Set<Collection>()
                .OrderByDescending(c => c.Items.Count)
                .Take(count)
                .ToListAsync();
        }
    }
}
