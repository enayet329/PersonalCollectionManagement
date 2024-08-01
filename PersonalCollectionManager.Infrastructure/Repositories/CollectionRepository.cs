

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Data;

namespace PersonalCollectionManager.Infrastructure.Repositories
{
    public class CollectionRepository : Repository<Collection>, ICollectionRepository    {
        public CollectionRepository(AppDbContext context, ILogger<Repository<Collection>> logger)
            : base(context, logger) { }

        public async Task<IEnumerable<Collection>> GetAllCollectionAsync()
        {
                return await _context.Set<Collection>().ToListAsync();
        }

        public async Task<Collection> GetCollectionByIdAsync(Guid id)
        {
            return await _context.Set<Collection>().FindAsync(id);
        }
    }
}
