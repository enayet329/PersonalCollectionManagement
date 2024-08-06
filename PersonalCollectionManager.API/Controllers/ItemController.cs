using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAllItemsAsync()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("tag/{name}")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsByTagAsync([FromRoute] string name)
        {
            var items = await _itemService.GetItemsByTagAsync(name);
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ItemDto>> GetItemByIdAsync([FromRoute] Guid id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound(new { message = "Item not found." });
            }
            return Ok(item);
        }

        [HttpGet("collection/{collectionId:guid}")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsByCollectionIdAsync([FromRoute] Guid collectionId)
        {
            var items = await _itemService.GetAllItemByCollectionIdAsync(collectionId);
            if (items == null || !items.Any())
            {
                return NotFound(new { message = "No items found for this collection." });
            }
            return Ok(items);
        }

        [HttpGet("recent")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetRecentItemsAsync()
        {
            var items = await _itemService.GetRecentItemsAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemAsync([FromBody] ItemRequestDto itemRequest)
        {
            var result = await _itemService.AddItemAsync(itemRequest);
            return CreatedAtAction(nameof(GetItemByIdAsync), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateItemAsync([FromRoute] Guid id, [FromBody] ItemDto itemDto)
        {
            if (id != itemDto.Id)
            {
                return BadRequest(new { message = "Item ID in the path and body do not match." });
            }

            var result = await _itemService.UpdateItemAsync(itemDto);
            if (result == null)
            {
                return NotFound(new { message = "Item not found." });
            }
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteItemAsync([FromRoute] Guid id)
        {
            var result = await _itemService.DeleteItemAsync(id);
            if (result == null)
            {
                return NotFound(new { message = "Item not found." });
            }
            return NoContent();
        }
    }
}
