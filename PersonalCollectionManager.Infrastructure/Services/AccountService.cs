﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.DTOs;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IAuthService;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;

        public AccountService(
            IUserRepository userRepository, 
            IAuthRepository authRepository, 
            IJwtTokenService jwtTokenService, 
            IConfiguration configuration,
            IMapper mapper, 
            ILogger<AccountService> logger)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
            _jwtTokenService = jwtTokenService;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult> Register(RegisterRequestDto userDTO)
        {
            try
            {

                if (await IsEmailAvailableAsync(userDTO.Email))
                {
                    return new OperationResult(false, "Email already taken.");
                }

                var user = _mapper.Map<User>(userDTO);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.PasswordHash);
                user.PreferredThemeDark = false;
                user.PreferredLanguage = "en";
                user.JoinedAt = DateTime.Now;

                await _userRepository.AddAsync(user);
                return new OperationResult(true, "User registered successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration.");
                return new OperationResult(false, "Error during user registration.");
            }
        }

        public async Task<OperationResult> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                if (loginRequestDTO == null)
                {
                    return new OperationResult(false, "Login request cannot be null.");
                }

                var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == loginRequestDTO.Email);

                if (user == null)
                {
                    return new OperationResult(false, "User doesn't exisit.");
                }
                else if (user.IsBlocked)
                {
                    return new OperationResult(false, "User is Blocked.");
                }

                if (!BCrypt.Net.BCrypt.Verify(loginRequestDTO.Password, user.PasswordHash))
                {
                    return new OperationResult(false, "Invalid Password.");
                }

                var refreshToken = await _authRepository.GetRefreshToken(user.Id);
                var accessToken = _jwtTokenService.GenerateToken(user);

                if(refreshToken != null && refreshToken.IsExpired)
                {
                    await _authRepository.Remove(refreshToken);
                }

                if (refreshToken == null )
                {
                    

                    var newRefreshToken = _jwtTokenService.GenerateRefreshToken();
                    var expiryTimeDays = _configuration.GetSection("JwtSettings:RefreshTokenExpiryDays").Value;

                    var refreshTokenEntity = new RefreshToken
                    {
                        Token = newRefreshToken,
                        UserId = user.Id,
                        Expires = DateTime.UtcNow.AddDays(int.Parse(expiryTimeDays!)),
                        Created = DateTime.UtcNow,
                    };

                    var result = await _authRepository.AddAsync(refreshTokenEntity);

                    if (result == null)
                    {
                        return new OperationResult(false, "Error saving refresh token.");
                    }

                    return new OperationResult(true, "Login successful", accessToken, newRefreshToken, user.PreferredLanguage, user.PreferredThemeDark);
                }

                return new OperationResult(true, "Login successful", accessToken, refreshToken.Token, user.PreferredLanguage, user.PreferredThemeDark);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user login.");
                return new OperationResult(false, "An error occurred during login.");
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
                return null;
            }
        }

        public async Task<UserDto> GetUserByUseremailAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(email);
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by email.");
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
                _logger.LogError(ex, "Error checking email availability.");
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking username availability.");
                throw;
            }
        }

        public async Task<OperationResult> UpdateUser(UserDto userDto)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userDto.Id);

                if (user == null)
                {
                    return new OperationResult(false, "User not found.");
                }
                user.PreferredThemeDark = userDto.PreffrredThemeDark;
                user.PreferredThemeDark = userDto.PreffrredThemeDark;
                user.Username = userDto.Username;
                user.ImageURL = userDto.ImageURL;
                user.PasswordHash = user.PasswordHash;

                var result = await _userRepository.Update(user);

                if (result == null)
                {
                    return new OperationResult(false, "Error updating user.");
                }
                return new OperationResult(true, "User updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user.");
                return new OperationResult(false, "Error updating user.");
            }
        }

        public async Task<OperationResult> GetRefreshToken(RefreshTokenRequestDto refreshToken)
        {
            try
            {
                var principal = _jwtTokenService.GetPrincipalFromToken(refreshToken.AccessToken);
                var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var refreshTokenEntity = await _authRepository.GetRefreshToken(Guid.Parse(userId!));

                if (refreshTokenEntity == null || refreshTokenEntity.Token != refreshToken.RefreshToken || refreshTokenEntity.IsExpired)
                {
                    return new OperationResult(false, "Invalid refresh token.");
                }

                var user = _mapper.Map<User>(await GetUserByIdAsync(Guid.Parse(userId!)));

                var accessToken = _jwtTokenService.GenerateToken(user);
                var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

                var expiryTimeDays = _configuration.GetSection("JwtSettings:RefreshTokenExpiryDays").Value;
                refreshTokenEntity.Token = newRefreshToken;
                refreshTokenEntity.Created = DateTime.UtcNow;
                refreshTokenEntity.Expires = DateTime.Now.AddDays(int.Parse(expiryTimeDays!));

                var result = await _authRepository.Update(refreshTokenEntity);

                if (result == null)
                {
                    return new OperationResult(false, "Error updating refresh token.");
                }
                return new OperationResult(true, "Login successful", accessToken, newRefreshToken, user.PreferredLanguage, user.PreferredThemeDark);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing access token.");
                return new OperationResult(false, "Error refreshing access token.");
            }
        }

        public async Task<bool> ChangeThemeAsync(Guid userId, bool isDarkMode)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                user.PreferredThemeDark = isDarkMode;
                var result = await _userRepository.Update(user);
                return result.PreferredThemeDark == isDarkMode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing theme.");
                return false;
            }
        }

        public async Task<bool> ChangeLanguageAsync(Guid userId, string language)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                user.PreferredLanguage = language;
                var result = await _userRepository.Update(user);
                return result.PreferredLanguage == language;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing language.");
                return false;
            }
        }
    }
}
