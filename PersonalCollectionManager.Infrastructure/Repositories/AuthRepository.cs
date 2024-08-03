using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Data;


namespace PersonalCollectionManager.Infrastructure.Repositories
{
    public class AuthRepository : Repository<RefreshToken>, IAuthRepository
    {
        public AuthRepository(AppDbContext context,ILogger<AuthRepository> logger) : base(context, logger) { }

        public async Task<RefreshToken?> GetRefreshToken(Guid userId)
        {
            return await _context.Set<RefreshToken>().FirstOrDefaultAsync(x => x.UserId == userId);
        }

    }
}
