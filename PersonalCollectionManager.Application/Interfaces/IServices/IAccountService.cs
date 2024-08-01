using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;


namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface IAccountService 
    {
        Task<OperationResult> Register(RegisterRequestDto userDto);
        Task<OperationResult> Login(LoginRequestDTO loginRequestDto);
        Task<UserDTO> GetUserByIdAsync(Guid id);
        Task<UserDTO> GetUserByUseremailAsync(string email);
        Task<bool> IsUsernameAvailableAsync(string username);
        Task<bool> IsEmailAvailableAsync(string email);
    }
}
