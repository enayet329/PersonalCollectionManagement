using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("get/items")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAllItemAsync()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("get/item/id")]
        public async Task<ActionResult<ItemDto>> GetItemByIdAsync(Guid id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            return Ok(item);
        }

        [HttpPost("add/item")]
        public async Task<IActionResult> AddItemAsync([FromBody] ItemRequestDto item)
        {
            var result = await _itemService.AddItemAsync(item);
            return Ok(result);
        }

        [HttpPut("update/item")]
        public async Task<IActionResult> UpdateItemAsync([FromBody] ItemUpdateRequestDto item)
        {
            var result = await _itemService.UpdateItemAsync(item);
            return Ok(result);
        }

        [HttpDelete("delete/item/id")]
        public async Task<IActionResult> DeleteItemAsync(Guid id)
        {
            var result = await _itemService.DeleteItemAsync(id);
            return Ok(result);
        }
    }
}

