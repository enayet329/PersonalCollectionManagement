using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Data;
using PersonalCollectionManager.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Data.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context, ILogger<Repository<Tag>> logger)
            : base(context, logger) { }

        public async Task<IEnumerable<Tag>> GetByItemId(Guid id)
        {
            return await _context.Set<ItemTag>()
                .Where(it => it.ItemId == id)
                .Select(it => it.Tag)
                .ToListAsync();
        }

        public Task<Tag?> GetTagWithItemTagAsync(Guid tagId)
        {
            return _context.Set<Tag>()
                        .SingleOrDefaultAsync(t => t.Id == tagId);
        }
    }
}
