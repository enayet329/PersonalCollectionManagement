
using PersonalCollectionManager.Domain.Entities;
using System.Security.Claims;

namespace PersonalCollectionManager.Application.Interfaces.IAuthService
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user, IList<Claim> additionalClaims = null);
    }
}
