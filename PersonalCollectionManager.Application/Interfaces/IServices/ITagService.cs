
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;


namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface ITagService 
    {
        Task<IEnumerable<TagDto>> GetTagsByItemIdAsync(Guid id);
        Task<IEnumerable<TagDto>> GetAllTagAsync();
        Task<IEnumerable<TagDto>> GetPopularTagsAsync();
        Task<TagDto> GetTagByIdAsync(Guid id);
        Task<OperationResult> AddTagAsync(IEnumerable<TagRequestDto> tag);
        Task<OperationResult> UpdateTagAsync(Guid itemId, IEnumerable<TagDto> tags);
        Task<OperationResult> DeleteTagAsync(Guid id);

    }
}
