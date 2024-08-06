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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await _accountService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(user);
        }

        [HttpGet("email")]
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
        public async Task<IActionResult> IsUsernameAvailable([FromQuery] string username)
        {
            var isAvailable = await _accountService.IsUsernameAvailableAsync(username);
            return Ok(new { available = isAvailable });
        }

        [HttpGet("availability/email")]
        public async Task<IActionResult> IsEmailAvailable([FromQuery] string email)
        {
            var isAvailable = await _accountService.IsEmailAvailableAsync(email);
            return Ok(new { available = isAvailable });
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpPut("{userId:guid}/language")]
        public async Task<IActionResult> UpdateLanguage([FromRoute] Guid userId, [FromQuery] string language)
        {
            var result = await _accountService.ChangeLanguageAsync(userId, language);
            if (result)
            {
                return Ok(new { message = "Language updated successfully." });
            }

            return BadRequest(new { message = "Language not updated." });
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpPut("{userId:guid}/theme")]
        public async Task<IActionResult> UpdateTheme([FromRoute] Guid userId, [FromQuery] bool theme)
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
