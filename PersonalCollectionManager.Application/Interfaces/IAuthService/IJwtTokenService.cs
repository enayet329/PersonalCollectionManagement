
namespace PersonalCollectionManager.Application.Interfaces.IAuthService
{
    public interface IJwtTokenService
    {
        string GenerateToken(string user);
        bool ValidateToken(string token);
    }
}
