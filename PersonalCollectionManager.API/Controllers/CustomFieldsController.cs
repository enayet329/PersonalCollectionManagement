using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Services;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/custom-field")]
    [Authorize(policy: "AdminOrUser")]
    [ApiController]
    public class CustomFieldsController : ControllerBase
    {
        private readonly ICustomFieldService _customFieldService;
        public CustomFieldsController(ICustomFieldService customFieldService)
        {
            _customFieldService = customFieldService;
        }

        [HttpGet("{collectionId}")]
        public async Task<IActionResult> GetCustomFields(Guid collectionId)
        {
            var customFields = await _customFieldService.GetCustomFieldsByCollectionIdAsync(collectionId);
            return Ok(customFields);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomField([FromBody] IEnumerable<CustomFieldCreateDto> customField)
        {
            var result = await _customFieldService.AddCustomFieldAsync(customField);
            return Ok(result ? true : false);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomField([FromBody] IEnumerable<CustomFieldUpdateDto> customField)
        {
            var result = await _customFieldService.UpdateCustomFieldAsync(customField);
            return Ok(result ? true : false);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomField(Guid id)
        {
            var result = await _customFieldService.DeleteCustomFieldAsync(id);
            return Ok(result ? true : false);
        }
    }
}
