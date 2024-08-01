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
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context, ILogger<Repository<Comment>> logger)
            : base(context, logger) { }


        public async Task<IEnumerable<Comment>> GetCommentsByItemAsync(Guid itemId)
        {
            return await _context.Set<Comment>().Where(c => c.ItemId == itemId).ToListAsync();
        }

    }
}
