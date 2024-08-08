using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/collections")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionDto>>> GetAllCollections()
        {
            var collections = await _collectionService.GetAllCollectionsAsync();
            return Ok(collections);
        }

        [HttpGet("largest")]
        public async Task<ActionResult<IEnumerable<CollectionDto>>> GetLargestCollections()
        {
            var collections = await _collectionService.GetLargestCollecitonAsync();
            return Ok(collections);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCollection([FromBody] CollectionRequestDto collectionRequest)
        {
            var result = await _collectionService.AddCollectionAsync(collectionRequest);
            return Ok(result);
        }

        [HttpDelete("delete/id")]
        public async Task<IActionResult> DeleteCollection([FromRoute] Guid id)
        {
            var result = await _collectionService.DeleteCollectionAsync(id);
            return Ok(result);
        }

        [HttpGet("get/id")]
        public async Task<IActionResult> GetCollectionById([FromRoute] Guid id)
        {
            var collection = await _collectionService.GetCollectionByIdAsync(id);

            if (collection == null)
            {
                return NotFound(new { message = "Collection not found." });
            }

            return Ok(collection);
        }

        [HttpGet("get/userId")]
        public async Task<IActionResult> GetCollectionsByUserId([FromRoute] Guid userId)
        {
            var collections = await _collectionService.GetAllCollectionsByUserIdAsync(userId);

            if (collections == null)
            {
                return NotFound(new { message = "Collections not found." });
            }

            return Ok(collections);
        }

        [HttpPut("update/id")]
        public async Task<IActionResult> UpdateCollection([FromRoute] Guid id, [FromBody] CollectionDto collectionUpdate)
        {
            collectionUpdate.Id = id;
            var result = await _collectionService.UpdateCollectionAsync(collectionUpdate);
            return Ok(result);
        }
    }
}
