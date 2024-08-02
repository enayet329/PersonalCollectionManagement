using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto user)
        {
            var result = await _accountService.Register(user);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO request)
        {
            var userDto = await _accountService.Login(request);

            if (userDto == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            return Ok(userDto);
        }

        [HttpGet("get/user/id")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _accountService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(user);
        }

        [HttpGet("get/user/email")]
        public async Task<IActionResult> GetUserByUsername(string email)
        {
            var user = await _accountService.GetUserByUseremailAsync(email);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(user);
        }

        [HttpGet("avaiable/usernames")]
        public async Task<IActionResult> GetAvailableUsernames(string username)
        {
            var availableUsernames = await _accountService.IsUsernameAvailableAsync(username);
            
            return Ok(availableUsernames);
        }

        [HttpGet("avaiable/emails")]
        public async Task<IActionResult> GetAvailableEmails(string email)
        {
            var availableEmails = await _accountService.IsEmailAvailableAsync(email);

            return Ok(availableEmails);
        }
    }
}
