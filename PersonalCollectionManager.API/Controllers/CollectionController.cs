using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;


namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;
        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionDTO>>> GetAllColleciton()
        {
            var user = await _collectionService.GetAllCollectionsAsync();
            return Ok(user);
        }

        [HttpPost("add/collection")]
        public async Task<IActionResult> AddCollection(CollectionRequestDto collection)
        {
            var result = await _collectionService.AddCollectionAsync(collection);
            return Ok(result);
        }

        [HttpDelete("delete/collection")]
        public async Task<IActionResult> DeleteCollection(Guid id)
        {
            var result = await _collectionService.DeleteCollectionAsync(id);
            return Ok(result);
        }

        [HttpGet("get/collection/id")]
        public async Task<IActionResult> GetCollectionById(Guid id)
        {
            var result = await _collectionService.GetCollectionByIdAsync(id);

            if (result == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(result);
        }

        [HttpPut("update/collection")]
        public IActionResult UpdateCollection(CollectionRequestDto collection)
        {
            var result = _collectionService.UpdateCollectionAsync(collection);
            return Ok(result);
        }
    }
}
