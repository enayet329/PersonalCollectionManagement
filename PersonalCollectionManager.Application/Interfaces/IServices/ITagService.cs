
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;


namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface ITagService 
    {
        // Add: new method to get all tags by item id
        Task<IEnumerable<TagDto>> GetTagsByItemIdAsync(Guid id);
        Task<IEnumerable<TagDto>> GetAllTagAsync();
        Task<IEnumerable<TagDto>> GetPopularTagsAsync();
        Task<TagDto> GetTagByIdAsync(Guid id);
        Task<OperationResult> AddTagAsync(TagRequestDto tag);
        Task<OperationResult> UpdateTagAsync(TagDto tag);
        Task<OperationResult> DeleteTagAsync(Guid id);

    }
}
