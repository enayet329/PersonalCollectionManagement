
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.Services
{
    public interface IAdminServices 
    {
        Task<OperationResult> AddAdminRoleAsync(Guid id);
        Task<OperationResult> RemoveAdminRoleAsync(Guid id);
        Task<IEnumerable<UserDTO>> GetAllUserAsync();
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<UserDTO> GetUserByIdAsync(Guid id);
        Task<OperationResult> BlockUserAsync(Guid id);
        Task<OperationResult> UnblockUserAsync(Guid id);
        Task<OperationResult> DeleteUserAsync(Guid id);
    }
}
