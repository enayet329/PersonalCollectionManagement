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
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto user)
        {
            var result = await _accountService.Register(user);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var userDto = await _accountService.Login(request);

            if (userDto == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            return Ok(userDto);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto token)
        {
            var userDto = await _accountService.GetRefreshToken(token);

            if (userDto == null)
            {
                return Unauthorized(new { message = "Invalid token." });
            }

            return Ok(userDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _accountService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(user);
        }

        [HttpGet("by-email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            var user = await _accountService.GetUserByUseremailAsync(email);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(user);
        }

        [HttpGet("availability/username")]
        public async Task<IActionResult> GetAvailableUsernames([FromQuery] string username)
        {
            var isAvailable = await _accountService.IsUsernameAvailableAsync(username);
            return Ok(new { available = isAvailable });
        }

        [HttpGet("availability/email")]
        public async Task<IActionResult> GetAvailableEmails([FromQuery] string email)
        {
            var isAvailable = await _accountService.IsEmailAvailableAsync(email);
            return Ok(new { available = isAvailable });
        }

        [HttpPut("{userId:guid}/language")]
        public async Task<IActionResult> ChangeLanguage([FromRoute] Guid userId, [FromQuery] string language)
        {
            var result = await _accountService.ChangeLanguageAsync(userId, language);
            if (result)
            {
                return Ok(new { message = "Language updated successfully." });
            }

            return Ok(new { message = "Language not updated." });
        }

        [HttpPut("{userId:guid}/theme")]
        public async Task<IActionResult> ChangeTheme([FromRoute] Guid userId, [FromQuery] bool theme)
        {
            var result = await _accountService.ChangeThemeAsync(userId, theme);
            if (result)
            {
                return Ok(new { message = "Theme updated successfully." });
            }

            return Ok(new { message = "Theme not updated." });
        }
    }
}
