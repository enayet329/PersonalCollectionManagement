using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
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

        public async Task<Like?> GetLikeByUserIdAndItemId(LikeRequestDto requestDto)
        {
            try
            {
                return await _context.Set<Like>().FirstOrDefaultAsync(l => l.ItemId == requestDto.ItemId && l.UserId == requestDto.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting likes by user id and item id");
                throw;
            }
        }

        public async Task<int> GetLikesByItemId(Guid itemId)
        {
            try
            {
                return (await _context.Set<Like>().Where(l => l.ItemId == itemId).ToListAsync()).Count();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting likes by item id");
                throw;
            }
        }
    }
}
