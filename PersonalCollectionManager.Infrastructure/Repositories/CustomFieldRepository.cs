using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Data;

namespace PersonalCollectionManager.Infrastructure.Repositories
{
    public class CustomFieldRepository : Repository<CustomField>, ICustomFieldRepository
    {
        public CustomFieldRepository(AppDbContext context, ILogger<CustomFieldRepository> logger) : base(context, logger) { }

        public async Task AddRangeAsync(IEnumerable<CustomField> customFields)
        {
            try
            {
                await _context.Set<CustomField>().AddRangeAsync(customFields);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding custom fields.");
                throw;
            }
        }

        public async Task<IEnumerable<CustomField>> GetByCollectionIdAsync(Guid collectionId)
        {
            try
            {
                return await _context.Set<CustomField>().Where(x => x.CollectionId == collectionId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting custom fields by collection ID.");
                throw;
            }
        }
    }
}
