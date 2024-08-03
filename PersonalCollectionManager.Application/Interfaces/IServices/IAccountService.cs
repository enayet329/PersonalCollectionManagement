using PersonalCollectionManager.Application.DTOs;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;


namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface IAccountService 
    {
        Task<OperationResult> Register(RegisterRequestDto userDto);
        Task<OperationResult> Login(LoginRequestDTO loginRequestDto);
        Task<OperationResult> GetRefreshToken(RefreshTokenRequestDto refreshToken);
        Task<OperationResult> UpdateUser(UserDto userDto);
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> GetUserByUseremailAsync(string email);
        Task<bool> IsUsernameAvailableAsync(string username);
        Task<bool> IsEmailAvailableAsync(string email);
        Task<bool> ChangeThemeAsync(Guid userId,bool isDarkMode);
        Task<bool> ChangeLanguageAsync(Guid userId,String language);
    }
}
