using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalCollectionManager.API.Controllers
{
    [Route("api/v1/custom-fields")]
    [Authorize(Policy = "AdminOrUser")]
    [ApiController]
    public class CustomFieldsController : ControllerBase
    {
        private readonly ICustomFieldService _customFieldService;

        public CustomFieldsController(ICustomFieldService customFieldService)
        {
            _customFieldService = customFieldService;
        }

        [HttpGet("collectionId")]
        public async Task<IActionResult> GetCustomFieldsByCollectionId(Guid collectionId)
        {
            var customFields = await _customFieldService.GetCustomFieldsByCollectionIdAsync(collectionId);
            return Ok(customFields);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCustomFields([FromBody] IEnumerable<CustomFieldCreateDto> customFields)
        {
            var result = await _customFieldService.AddCustomFieldAsync(customFields);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomFields([FromBody] IEnumerable<CustomFieldUpdateDto> customFields)
        {
            var result = await _customFieldService.UpdateCustomFieldAsync(customFields);
            return Ok(result);
        }

        [HttpDelete("delete/id")]
        public async Task<IActionResult> DeleteCustomField( Guid customFieldId)
        {
            var result = await _customFieldService.DeleteCustomFieldAsync(customFieldId);
            return Ok(result);
        }
    }
}
