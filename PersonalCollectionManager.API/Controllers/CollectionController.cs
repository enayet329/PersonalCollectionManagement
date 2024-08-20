using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using PersonalCollectionManager.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/collections")]
    [ApiController]
    [Authorize(Policy = "AdminOrUser")]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;
        private readonly ICategoryService _categoryService;

        public CollectionController(ICollectionService collectionService, ICategoryService categoryService)
        {
            _collectionService = collectionService;
            _categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CollectionDto>>> GetAllCollections()
        {
            var collections = await _collectionService.GetAllCollectionsAsync();
            return Ok(collections);
        }

        [HttpGet("largest")]
        [AllowAnonymous]
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
        public async Task<IActionResult> DeleteCollection(Guid id)
        {
            var result = await _collectionService.DeleteCollectionAsync(id);
            return Ok(result);
        }

        [HttpGet("get/id")]
        public async Task<IActionResult> GetCollectionById(Guid id)
        {
            var collection = await _collectionService.GetCollectionByIdAsync(id);

            if (collection == null)
            {
                return NotFound(new { message = "Collection not found." });
            }

            return Ok(collection);
        }

        [HttpGet("get/userId")]
        public async Task<IActionResult> GetCollectionsByUserId(Guid userId)
        {
            var collections = await _collectionService.GetAllCollectionsByUserIdAsync(userId);

            if (collections == null)
            {
                return NotFound(new { message = "Collections not found." });
            }

            return Ok(collections);
        }

        [HttpPut("update/id")]
        public async Task<IActionResult> UpdateCollection([FromBody] CollectionUpdateDto collectionUpdate)
        {
            var result = await _collectionService.UpdateCollectionAsync(collectionUpdate);
            return Ok(result);
        }


        [HttpGet("get/categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }

    }
}
