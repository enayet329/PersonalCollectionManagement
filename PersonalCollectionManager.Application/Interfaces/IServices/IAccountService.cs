using PersonalCollectionManager.Application.DTOs;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;


namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface IAccountService 
    {
        Task<OperationResult> Register(RegisterRequestDto userDto);
        Task<OperationResult> Login(LoginRequestDTO loginRequestDto);
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> GetUserByUseremailAsync(string email);
        Task<bool> IsUsernameAvailableAsync(string username);
        Task<bool> IsEmailAvailableAsync(string email);
    }
}
