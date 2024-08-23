using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/items")]
    [Authorize(Policy = "AdminOrUser")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }


        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAllItemsAsync()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("get/tagName")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsByTagAsync(string name)
        {
            var items = await _itemService.GetItemsByTagAsync(name);
            return Ok(items);
        }

        [HttpGet("get/id")]
        public async Task<ActionResult<ItemDto>> GetItemByIdAsync(Guid id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound(new { message = "Item not found." });
            }
            return Ok(item);
        }

        [HttpGet("get/collectionId")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsByCollectionIdAsync(Guid collectionId)
        {
            var items = await _itemService.GetAllItemByCollectionIdAsync(collectionId);
            if (items == null || !items.Any())
            {
                return NotFound(new { message = "No items found for this collection." });
            }
            return Ok(items);
        }

        [HttpGet("recent")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetRecentItemsAsync()
        {
            var items = await _itemService.GetRecentItemsAsync();
            return Ok(items);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddItemAsync([FromBody] ItemRequestDto itemRequest)
        {
            var result = await _itemService.AddItemAsync(itemRequest);

            if (result == null)
            {
                return NotFound(new { message = "Error adding item." });
            }


            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateItemAsync([FromBody] ItemUpdateRequestDto itemDto)
        {

            var result = await _itemService.UpdateItemAsync(itemDto);
            if (result == null)
            {
                return NotFound(new { message = "Item not found." });
            }


            return Ok(result);
        }

        [HttpDelete("delete/id")]
        public async Task<IActionResult> DeleteItemAsync(Guid id)
        {
            var result = await _itemService.DeleteItemAsync(id);
            if (result == null)
            {
                return NotFound(new { message = "Item not found." });
            }

            return Ok(result);
        }
    }
}
