using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalCollectionManager.API.Controllers
{
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    [Route("api/v1/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminService;
        private readonly ICollectionService _collectionService;
        private readonly IItemService _itemService;

        public AdminController(IAdminServices adminService, ICollectionService collectionService, IItemService itemService)
        {
            _adminService = adminService;
            _collectionService = collectionService;
            _itemService = itemService;
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _adminService.GetAllUserAsync();
            return Ok(users);
        }

        [HttpPost("collections")]
        public async Task<IActionResult> CreateCollectionAsync([FromBody] CollectionRequestDto collectionDto)
        {
            var response = await _collectionService.AddCollectionAsync(collectionDto);
            return Ok(response);
        }

        [HttpPost("items")]
        public async Task<IActionResult> CreateItemAsync([FromBody] ItemRequestDto itemDto)
        {
            var response = await _itemService.AddItemAsync(itemDto);
            return Ok(response);
        }

        [HttpGet("user/id")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _adminService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            return Ok(user);
        }

        [HttpGet("user/email")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _adminService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            return Ok(user);
        }

        [HttpPut("user/upgrade/admin")]
        public async Task<IActionResult> AddAdminRole( Guid id)
        {
            var user = await _adminService.AddAdminRoleAsync(id);
            return Ok(user);
        }

        [HttpPut("user/downgrade/admin")]
        public async Task<IActionResult> RemoveAdminRole( Guid id)
        {
            var user = await _adminService.RemoveAdminRoleAsync(id);
            return Ok(user);
        }

        [HttpDelete("delete/user")]
        public async Task<IActionResult> DeleteUser( Guid id)
        {
            var user = await _adminService.DeleteUserAsync(id);
            return Ok(user);
        }

        [HttpPut("user/block")]
        public async Task<IActionResult> BlockUser( Guid id)
        {
            var response = await _adminService.BlockUserAsync(id);
            return Ok(response);
        }

        [HttpPut("users/unblock")]
        public async Task<IActionResult> UnblockUser(Guid id)
        {
            var response = await _adminService.UnblockUserAsync(id);
            return Ok(response);
        }
    }
}
