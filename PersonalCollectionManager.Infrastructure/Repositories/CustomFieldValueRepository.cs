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
                return await _context.Set<CustomFieldValue>()
                    .Include(x => x.CustomField)
                    .Where(x => x.ItemId == itemId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting custom field values by item ID.");
                throw;
            }
        }

        public async Task<bool> UpdateCustomFieldValueAsync(Guid itemId, IEnumerable<CustomFieldValue> customFieldValues)
        {
            try
            {
                var item = await _context.Set<Item>().FindAsync(itemId);
                if (item == null)
                {
                    throw new ArgumentException("Item not found.");
                }

                var existingCustomFieldValues = await _context.Set<CustomFieldValue>()
                    .Include(x => x.CustomField)
                    .Where(x => x.ItemId == itemId)
                    .ToListAsync();

                var existingSet = new HashSet<CustomFieldValue>(existingCustomFieldValues);
                var newSet = new HashSet<CustomFieldValue>(customFieldValues);

                var deleteCustomFieldValues = existingSet.Except(newSet);
                _context.Set<CustomFieldValue>().RemoveRange(deleteCustomFieldValues);

                var addCustomFieldValues = newSet.Except(existingSet);
                _context.Set<CustomFieldValue>().AddRange(addCustomFieldValues);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating custom field values.");
                throw;
            }
        }

    }
}
