using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using System;
using System.Threading.Tasks;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            var result = await _accountService.Register(registerRequest);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var userDto = await _accountService.Login(loginRequest);

            if (userDto == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            return Ok(userDto);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto refreshTokenRequest)
        {
            var userDto = await _accountService.GetRefreshToken(refreshTokenRequest);

            if (userDto == null)
            {
                return Unauthorized(new { message = "Invalid token." });
            }

            return Ok(userDto);
        }

        [HttpGet("user/id")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _accountService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(user);
        }

        [HttpGet("user/email")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _accountService.GetUserByUseremailAsync(email);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(user);
        }

        [HttpGet("availability/username")]
        public async Task<IActionResult> IsUsernameAvailable(string username)
        {
            var isAvailable = await _accountService.IsUsernameAvailableAsync(username);
            return Ok(new { available = isAvailable });
        }

        [HttpGet("availability/email")]
        public async Task<IActionResult> IsEmailAvailable(string email)
        {
            var isAvailable = await _accountService.IsEmailAvailableAsync(email);
            return Ok(new { available = isAvailable });
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpPut("language/userId")]
        public async Task<IActionResult> UpdateLanguage(Guid userId, string language)
        {
            var result = await _accountService.ChangeLanguageAsync(userId, language);
            if (result)
            {
                return Ok(new { message = "Language updated successfully." });
            }

            return BadRequest(new { message = "Language not updated." });
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpPut("theme/userId")]
        public async Task<IActionResult> UpdateTheme(Guid userId, bool theme)
        {
            var result = await _accountService.ChangeThemeAsync(userId, theme);
            if (result)
            {
                return Ok(new { message = "Theme updated successfully." });
            }

            return BadRequest(new { message = "Theme not updated." });
        }
    }
}
