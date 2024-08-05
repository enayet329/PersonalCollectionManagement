using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Application.Interfaces.IServices;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Infrastructure.Services
{
    public class CustomFieldService : ICustomFieldService
    {
        private readonly ICustomFieldRepository _customFieldRepository;
        private readonly ICustomFieldValueRepository _customFieldValueRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomFieldService> _logger;

        public CustomFieldService(
            ICustomFieldRepository customFieldRepository,
            ICustomFieldValueRepository customFieldValueRepository,
            IMapper mapper,
            ILogger<CustomFieldService> logger)
        {
            _customFieldRepository = customFieldRepository;
            _customFieldValueRepository = customFieldValueRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddCustomFieldAsync(IEnumerable<CustomFieldCreateDto> customFieldDtos)
        {
            try
            {
                var customFields = _mapper.Map<IEnumerable<CustomField>>(customFieldDtos);
                await _customFieldRepository.AddRangeAsync(customFields);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding custom fields.");
                throw;
            }
        }

        public async Task<bool> AddCustomFieldValueAsync(IEnumerable<CustomFieldValueCreateDto> customFieldValueDtos)
        {
            try
            {
                var customFieldValues = _mapper.Map<IEnumerable<CustomFieldValue>>(customFieldValueDtos);
                await _customFieldValueRepository.AddRangeAsync(customFieldValues);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding custom field values.");
                throw;
            }
        }

        public async Task<bool> DeleteCustomFieldAsync(Guid id)
        {
            try
            {
                var customField = await _customFieldRepository.GetByIdAsync(id);
                if (customField != null)
                {
                    await _customFieldRepository.Remove(customField);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting custom field.");
                throw;
            }
        }

        public async Task<bool> DeleteCustomFieldValueAsync(Guid id)
        {
            try
            {
                var customFieldValue = await _customFieldValueRepository.GetByIdAsync(id);
                if (customFieldValue != null)
                {
                    await _customFieldValueRepository.Remove(customFieldValue);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting custom field value.");
                throw;
            }
        }

        public async Task<IEnumerable<CustomFieldDto>> GetCustomFieldsByCollectionIdAsync(Guid collectionId)
        {
            try
            {
                var customFields = await _customFieldRepository.GetByCollectionIdAsync(collectionId);
                return _mapper.Map<IEnumerable<CustomFieldDto>>(customFields);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting custom fields by collection ID.");
                throw;
            }
        }

        public async Task<IEnumerable<CustomFieldValueDto>> GetCustomFieldValuesByItemIdAsync(Guid itemId)
        {
            try
            {
                var customFieldValues = await _customFieldValueRepository.GetByItemIdAsync(itemId);
                return _mapper.Map<IEnumerable<CustomFieldValueDto>>(customFieldValues);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting custom field values by item ID.");
                throw;
            }
        }

        public async Task<bool> UpdateCustomFieldAsync(IEnumerable<CustomFieldUpdateDto> customFieldDtos)
        {
            try
            {
                var customFields = _mapper.Map<IEnumerable<CustomField>>(customFieldDtos);
                await _customFieldRepository.UpdateRangeAsync(customFields);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating custom fields.");
                throw;
            }
        }

        public async Task<bool> UpdateCustomFieldValueAsync(IEnumerable<CustomFieldValueUpdateDto> customFieldValueDtos)
        {
            try
            {
                var customFieldValues = _mapper.Map<IEnumerable<CustomFieldValue>>(customFieldValueDtos);
                await _customFieldValueRepository.UpdateRangeAsync(customFieldValues);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating custom field values.");
                throw;
            }
        }
    }
}
