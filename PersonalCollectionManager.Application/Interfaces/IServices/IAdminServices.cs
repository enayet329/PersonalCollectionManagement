
using PersonalCollectionManager.Application.DTOs;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.Services
{
    public interface IAdminServices 
    {
        Task<OperationResult> AddAdminRoleAsync(Guid id);
        Task<OperationResult> RemoveAdminRoleAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllUserAsync();
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<OperationResult> BlockUserAsync(Guid id);
        Task<OperationResult> UnblockUserAsync(Guid id);
        Task<OperationResult> DeleteUserAsync(Guid id);
    }
}
