using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Data;
using PersonalCollectionManager.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Data.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(AppDbContext context, ILogger<Repository<Item>> logger)
            : base(context, logger) { }


        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _context.Set<Item>().ToListAsync();
        }

        public async Task<Item?> GetItemByIdAsync(Guid id)
        {
            return await _context.Set<Item>().FindAsync(id);
        }
    }
}
