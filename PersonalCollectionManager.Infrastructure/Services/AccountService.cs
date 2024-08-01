using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;
using System.Linq.Expressions;

namespace PersonalCollectionManager.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;
        public AccountService(IUserRepository userRepository, IMapper mapper,ILogger<AccountService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }


        //TODO Implement register logic 
        public async Task<OperationResult> Register(RegisterRequestDto userDTO)
        {
            try
            {
                //TODO Validate userDTO
                var users = _mapper.Map<User>(userDTO);
                var user =  GetUserByIdAsync(users.Id);
                if(user.Result == null)
                {
                    users.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.PasswordHash);
                    await _userRepository.AddAsync(users);
                    return new OperationResult(true, "User registered successfully.");
                }
                return new OperationResult(false, "User already exists.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration.");
                throw;
            }
        }

        //TODO Implement login logic
        public async Task<OperationResult> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == loginRequestDTO.Email && u.PasswordHash == loginRequestDTO.Password);

                if (user == null)
                {
                    return new OperationResult(false, "Invalid email or password");
                }
                
                return new OperationResult(true, "Login successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user login.");
                throw;
            }
        }



        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error getting user by id");
                throw;
            }
        }

        public async Task<UserDTO> GetUserByUseremailAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(email);
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by email");
                throw;
            }
        }

        public async Task<bool> IsEmailAvailableAsync(string email)
        {
            try
            {
                var users = await _userRepository.FindAsync(u => u.Email == email);
                return users.Any();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by email");
                throw;
            }
        }

        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            try
            {
                var users = await _userRepository.FindAsync(u => u.Username == username);
                return users.Any();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting user by username");
                throw;
            }
        }

    }
}
