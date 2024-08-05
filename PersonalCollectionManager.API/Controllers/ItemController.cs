using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;

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
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAllItemAsync()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("tag/{name}")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemByTagIdAsync(string name)
        {
            var item = await _itemService.GetItemsByTagAsync(name);
            return Ok(item);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ItemDto>> GetItemByIdAsync(Guid id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            return Ok(item);
        }

        [HttpGet("collection/{collectionId:guid}")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemByCollectionIdAsync(Guid id)
        {
            var item = await _itemService.GetAllItemByCollectionIdAsync(id);
            return Ok(item);
        }

        [HttpGet("recent")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemByNameAsync()
        {
            var item = await _itemService.GetRecentItemsAsync();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemAsync([FromBody] ItemRequestDto item)
        {
            var result = await _itemService.AddItemAsync(item);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateItemAsync([FromBody] ItemDto item)
        {
            var result = await _itemService.UpdateItemAsync(item);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteItemAsync(Guid id)
        {
            var result = await _itemService.DeleteItemAsync(id);
            return Ok(result);
        }
    }
}

