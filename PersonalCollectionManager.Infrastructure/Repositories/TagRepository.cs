using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Data;
using PersonalCollectionManager.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Data.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context, ILogger<Repository<Tag>> logger)
            : base(context, logger) { }


        public async Task<Tag?> GetTagByNameAsync(string name)
        {
            return await _context.Set<Tag>().FirstOrDefaultAsync(t => t.Name == name);
        }
    }
}
