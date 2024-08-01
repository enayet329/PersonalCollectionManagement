using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Data;
using PersonalCollectionManager.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Data.Repositories
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        public LikeRepository(AppDbContext context, ILogger<Repository<Like>> logger)
            : base(context, logger) { }

        public async Task<IEnumerable<Like>> GetLikeByUserAndItemAsync(Guid itemId)
        {
            return await _context.Set<Like>().Where(l => l.ItemId == itemId).ToListAsync();
        }
    }
}
