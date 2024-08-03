

using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IRepository
{
    public interface IAuthRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> GetRefreshToken(Guid userId);

    }
}
