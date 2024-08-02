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
                var userName = await IsUsernameAvailableAsync(userDTO.Username);
                var email = await IsEmailAvailableAsync(userDTO.Email);
                if (userName == false && email == false)
                {
                    var users = _mapper.Map<User>(userDTO);
                    users.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.PasswordHash);
                    await _userRepository.AddAsync(users);
                    return new OperationResult(true, "User registered successfully.");
                }
                else if (userName == true)
                {
                    return new OperationResult(false, "Username already taken.");
                }
                return new OperationResult(false, "Useremail already taken.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration.");
                return null;
            }
        }


        //TODO Implement login logic and jwt token generation
        public async Task<OperationResult> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == loginRequestDTO.Email);

                if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequestDTO.Password, user.PasswordHash))
                {
                    return new OperationResult(false, "Invalid email or password");
                }

                return new OperationResult(true, "Login successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user login.");
                return null;
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
                return null;
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
                return null;
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
