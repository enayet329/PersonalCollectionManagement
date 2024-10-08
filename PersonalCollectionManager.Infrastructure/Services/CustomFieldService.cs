﻿using AutoMapper;
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
                if (customFieldValueDtos == null || !customFieldValueDtos.Any())
                {
                    _logger.LogWarning("No custom field values provided.");
                    return false;
                }

                var customFieldId = customFieldValueDtos.ElementAt(0)?.CustomFieldId;
                if (customFieldId == null)
                {
                    _logger.LogWarning("CustomFieldId is null.");
                    return false;
                }

                var customFieldExists = await _customFieldRepository.FindAsync(x => x.Id == customFieldId);
                if (customFieldExists == null)
                {
                    _logger.LogWarning($"Custom field with ID {customFieldId} does not exist.");
                    return false;
                }

                var customFieldValues = _mapper.Map<IEnumerable<CustomFieldValue>>(customFieldValueDtos);
                await _customFieldValueRepository.AddRangeAsync(customFieldValues);
                await _customFieldValueRepository.SaveChangesAsync();
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
        public async Task<bool> UpdateCustomFieldsAsync(Guid collectionId,IEnumerable<CustomFieldUpdateDto> customFieldDtos)
        {
            try
            {
                var customFields = _mapper.Map<IEnumerable<CustomField>>(customFieldDtos);
                var existingCustomFields = await _customFieldRepository.FindAsync(cl => cl.CollectionId == collectionId);

                var customFieldIdsToUpdate = customFields.Select(cf => cf.Id).ToList();
                var existingCustomFieldIds = existingCustomFields.Select(cf => cf.Id).ToList();

      
                var customFieldsToDelete = existingCustomFieldIds.Except(customFieldIdsToUpdate).ToList();
                foreach (var id in customFieldsToDelete)
                {
                    await DeleteCustomFieldAsync(id);
                }

        
                foreach (var customField in customFields)
                {
                    if (existingCustomFieldIds.Contains(customField.Id))
                    {
                        await _customFieldRepository.Update(customField);
                    }
                    else
                    {
                        await _customFieldRepository.AddAsync(customField);
                    }
                }

                await _customFieldRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating custom fields.");
                throw;
            }
        }


        public async Task<bool> UpdateCustomFieldValueAsync(Guid itemId, IEnumerable<CustomFieldValueUpdateDto> customFieldValueDtos)
        {
            try
            {
                if (customFieldValueDtos == null || !customFieldValueDtos.Any())
                {
                    return false;
                }
                var customFieldValues = _mapper.Map<IEnumerable<CustomFieldValue>>(customFieldValueDtos);
                var result = await _customFieldValueRepository.UpdateCustomFieldValueAsync(itemId, customFieldValues);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating custom field values.");
                throw;
            }
        }
    }
}
