using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Data;
using PersonalCollectionManager.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context, ILogger<Repository<User>> logger)
            : base(context, logger) { }


        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> GetUserByUserName(string userName)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Username == userName);
        }

        public async Task<User?> GetUserByUserNameAsync(string username)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Username == username);
        }

    }
}
