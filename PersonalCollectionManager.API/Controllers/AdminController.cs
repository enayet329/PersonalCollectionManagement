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
    [Authorize(policy: "AdminOnly")]
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

        [HttpPost("create/collection")]
        public async Task<IActionResult> CreateCollectionForUserAsync([FromBody] CollectionRequestDto collectionDto)
        {
            var response = await _collectionService.AddCollectionAsync(collectionDto);
            return Ok(response);
        }

        [HttpPost("create/item")]
        public async Task<IActionResult> CreateItemForCollectionAsync([FromBody] ItemRequestDto itemDto)
        {
            var response = await _itemService.AddItemAsync(itemDto);
            return Ok(response);
        }

        [HttpGet("users/{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await _adminService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            return Ok(user);
        }

        [HttpGet("users/by-email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            var user = await _adminService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            return Ok(user);
        }

        [HttpPut("users/{id:guid}/admin")]
        public async Task<IActionResult> UpdateUserAsAdmin([FromRoute] Guid id)
        {
            var user = await _adminService.AddAdminRoleAsync(id);
            return Ok(user);
        }

        [HttpPatch("users/{id:guid}/admin")]
        public async Task<IActionResult> RemoveUserAsAdmin([FromRoute] Guid id)
        {
            var user = await _adminService.RemoveAdminRoleAsync(id);
            return Ok(user);
        }

        [HttpDelete("users/{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await _adminService.DeleteUserAsync(id);
            return Ok(user);
        }

        [HttpPut("users/{id:guid}/block")]
        public async Task<IActionResult> BlockUser([FromRoute] Guid id)
        {
            var response = await _adminService.BlockUserAsync(id);
            return Ok(response);
        }

        [HttpPut("users/{id:guid}/unblock")]
        public async Task<IActionResult> UnblockUser([FromRoute] Guid id)
        {
            var response = await _adminService.UnblockUserAsync(id);
            return Ok(response);
        }
    }
}
