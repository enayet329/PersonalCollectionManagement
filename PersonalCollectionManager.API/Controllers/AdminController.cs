using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Application.Interfaces.Services;

namespace PersonalCollectionManager.API.Controllers
{
    [ApiController]
    [Authorize(policy: "AdminOnly")]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> CreateCollectionForUserAsync(CollectionRequestDto collectionDto)
        {
            var response = await _collectionService.AddCollectionAsync(collectionDto);
            return Ok(response);
        }

        [HttpPost("create/item")]
        public async Task<IActionResult> CreateItemForCollectionAsync(ItemRequestDto itemDto)
        {
            var response = await _itemService.AddItemAsync(itemDto);
            return Ok(response);
        }

        [HttpGet("get/user/id")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _adminService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("get/user/email")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _adminService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("update/admin")]
        public async Task<IActionResult> UpdateUserAsAdmin(Guid id)
        {
            var user = await _adminService.AddAdminRoleAsync(id);
            return Ok(user);
        }

        [HttpPut("remove/admin")]
        public async Task<IActionResult> RemoveUserAsAdmin(Guid id)
        {
            var user = await _adminService.RemoveAdminRoleAsync(id);
            return Ok(user);
        }

        [HttpDelete("delete/user")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _adminService.DeleteUserAsync(id);
            return Ok(user);
        }

        [HttpPut("block/user")]
        public async Task<IActionResult> BlockUser(Guid id)
        {
            var response = await _adminService.BlockUserAsync(id);
            return Ok(response);
        }

        [HttpPut("unblock/user")]
        public async Task<IActionResult> UnblockUser(Guid id)
        {
            var response = await _adminService.UnblockUserAsync(id);
            return Ok(response);
        }
    }
}

/*
 * The AdminController class is responsible for handling all the admin related requests.
 * It has methods for creating, updating, deleting, blocking, unblocking, and getting users.
 * It also has methods for creating and adding items to collections.
 */