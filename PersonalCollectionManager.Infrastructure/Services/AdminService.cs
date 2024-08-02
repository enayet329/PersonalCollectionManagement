
using AutoMapper;
using Azure;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.Interfaces.Services;
using PersonalCollectionManager.Domain.Entities;
using System.Linq.Expressions;

namespace PersonalCollectionManager.Infrastructure.Services
{
    public class AdminService : IAdminServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminService> _logger;
        public AdminService(IUserRepository userRepository, IMapper mapper, ILogger<AdminService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }




        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<UserDto>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users.");
                throw;
            }
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(email);
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by email.");
                throw;
            }
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by id.");
                throw;
            }
        }

        public async Task<OperationResult> BlockUserAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user != null && user.IsBlocked == false)
                {
                    user.IsBlocked = true;
                    _userRepository.Update(user);

                    return new OperationResult(true, "User blocked successfully.");
                }
                return new OperationResult(false, "User is already blocked.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error blocking user.");
                return new OperationResult(false, "Error blocking user.");
            }
        }

        public async Task<OperationResult> UnblockUserAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user != null && user.IsBlocked == true)
                {
                    user.IsBlocked = false;
                    _userRepository.Update(user);

                    return new OperationResult(true, "User unblocked successfully.");
                }
                return new OperationResult(false, "User is already unblocked.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error unblocking user.");
                return new OperationResult(false, "Error unblocking user.");
            }
        }
        public async Task<OperationResult> DeleteUserAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user != null)
                {
                    _userRepository.Remove(user);

                    return new OperationResult(true, "User deleted successfully.");
                }
                return new OperationResult(false, "User not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Delete User");
                return new OperationResult(false, "Error Delete User");
            }
        }

        public async Task<OperationResult> AddAdminRoleAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user != null && user.IsAdmin == false)
                {
                    user.IsAdmin = true;
                    _userRepository.Update(user);


                    return new OperationResult(true, "User is Admin.");
                }
                return new OperationResult(false, "User is already Admin.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Admin Role");
                return new OperationResult(false, "Error Admin Role");
            }
        }

        public async Task<OperationResult> RemoveAdminRoleAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user != null && user.IsAdmin == true)
                {
                    user.IsAdmin = false;
                    _userRepository.Update(user);

                    return new OperationResult(true, "User is not Admin.");
                }
                return new OperationResult(false, "User is not Admin.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Remove Admin Role");
                return new OperationResult(false, "Error Remove Admin Role");
            }
        }
    }
}
