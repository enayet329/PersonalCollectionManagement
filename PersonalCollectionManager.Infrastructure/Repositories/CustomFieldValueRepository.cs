using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Data;

namespace PersonalCollectionManager.Infrastructure.Repositories
{
    public class CustomFieldValueRepository : Repository<CustomFieldValue>, ICustomFieldValueRepository
    {
        public CustomFieldValueRepository(AppDbContext context, ILogger<CustomFieldValueRepository> logger) : base(context, logger) { }

        public async Task<IEnumerable<CustomFieldValue>> GetByItemIdAsync(Guid itemId)
        {
            try
            {
                return await _context.Set<CustomFieldValue>().Where(x => x.ItemId == itemId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting custom field values by item ID.");
                throw;
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<CustomFieldValue> customFieldValues)
        {
            try
            {
                _context.Set<CustomFieldValue>().UpdateRange(customFieldValues);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating custom field values.");
                throw;
            }
        }
    }
}
