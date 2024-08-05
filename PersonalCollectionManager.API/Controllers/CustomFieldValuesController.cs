using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/custom-field-valus")]
    [ApiController]
    public class CustomFieldValuesController : ControllerBase
    {
        private readonly ICustomFieldService _customFieldService;
        public CustomFieldValuesController(ICustomFieldService customFieldService)
        {
            _customFieldService = customFieldService;
        }


        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetCustomFieldValues(Guid itemId)
        {
            var customFieldValues = await _customFieldService.GetCustomFieldValuesByItemIdAsync(itemId);
            return Ok(customFieldValues);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomFieldValue([FromBody] IEnumerable<CustomFieldValueCreateDto> customFieldValue)
        {
            await _customFieldService.AddCustomFieldValueAsync(customFieldValue);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomFieldValue([FromBody] IEnumerable<CustomFieldValueUpdateDto> customFieldValue)
        {
            await _customFieldService.UpdateCustomFieldValueAsync(customFieldValue);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomFieldValue(Guid id)
        {
            await _customFieldService.DeleteCustomFieldValueAsync(id);
            return Ok();
        }
    }
}
