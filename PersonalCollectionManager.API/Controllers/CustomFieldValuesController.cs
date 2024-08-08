using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/custom-field-values")]
    [Authorize(Policy = "AdminOrUser")]
    [ApiController]
    public class CustomFieldValuesController : ControllerBase
    {
        private readonly ICustomFieldService _customFieldService;

        public CustomFieldValuesController(ICustomFieldService customFieldService)
        {
            _customFieldService = customFieldService;
        }

        [HttpGet("itemId")]
        public async Task<IActionResult> GetCustomFieldValuesByItemId([FromRoute] Guid itemId)
        {
            var customFieldValues = await _customFieldService.GetCustomFieldValuesByItemIdAsync(itemId);
            return Ok(customFieldValues);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCustomFieldValues([FromBody] IEnumerable<CustomFieldValueCreateDto> customFieldValues)
        {
            var result = await _customFieldService.AddCustomFieldValueAsync(customFieldValues);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomFieldValues([FromBody] IEnumerable<CustomFieldValueUpdateDto> customFieldValues)
        {
            var result = await _customFieldService.UpdateCustomFieldValueAsync(customFieldValues);
            return Ok(result);
        }

        [HttpDelete("delete/id")]
        public async Task<IActionResult> DeleteCustomFieldValue([FromRoute] Guid customFieldValueId)
        {
            var result = await _customFieldService.DeleteCustomFieldValueAsync(customFieldValueId);
            return Ok(result);
        }
    }
}
