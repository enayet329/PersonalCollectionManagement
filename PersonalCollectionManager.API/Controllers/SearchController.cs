using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly AlgoliaItemService _algoliaItemService;
        public SearchController(AlgoliaItemService algoliaItemService)
        {
            _algoliaItemService = algoliaItemService;
        }

        [HttpGet("full-text-search")]
        public async Task<IActionResult> Search(string query)
        {
            var items = await _algoliaItemService.SearchAsync<AlgoliaItemDto>(query);

            return Ok(items.Hits);
        }
    }
}
