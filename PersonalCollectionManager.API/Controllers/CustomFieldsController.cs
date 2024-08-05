using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;
using PersonalCollectionManager.Infrastructure.Services;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/custom-field")]
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
            await _customFieldService.AddCustomFieldAsync(customField);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomField([FromBody] IEnumerable<CustomFieldUpdateDto> customField)
        {
            await _customFieldService.UpdateCustomFieldAsync(customField);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomField(Guid id)
        {
            await _customFieldService.DeleteCustomFieldAsync(id);
            return Ok();
        }
    }
}
