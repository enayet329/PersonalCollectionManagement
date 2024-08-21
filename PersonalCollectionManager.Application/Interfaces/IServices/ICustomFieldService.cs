
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface ICustomFieldService
    {
        Task<IEnumerable<CustomFieldDto>> GetCustomFieldsByCollectionIdAsync(Guid collectionId);
        Task<bool> AddCustomFieldAsync(IEnumerable<CustomFieldCreateDto> customField);
        Task<bool> UpdateCustomFieldsAsync(Guid collectionId, IEnumerable<CustomFieldUpdateDto> customFieldDtos);
        Task<bool> DeleteCustomFieldAsync(Guid id);

        Task<IEnumerable<CustomFieldValueDto>> GetCustomFieldValuesByItemIdAsync(Guid itemId);
        Task<bool> AddCustomFieldValueAsync(IEnumerable<CustomFieldValueCreateDto> customFieldValue);
        Task<bool> UpdateCustomFieldValueAsync(IEnumerable<CustomFieldValueUpdateDto> customFieldValue);
        Task<bool> DeleteCustomFieldValueAsync(Guid id);
    }
}
