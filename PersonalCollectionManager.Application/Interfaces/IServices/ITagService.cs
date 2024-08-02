
using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;


namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface ITagService 
    {
        // TODO: Add new method 
        Task<IEnumerable<TagDto>> GetAllTagAsync();
        Task<TagDto> GetTagByIdAsync(Guid id);
        Task<OperationResult> AddTagAsync(TagRequestDto tag);
        Task<OperationResult> UpdateTagAsync(TagDto tag);
        Task<OperationResult> DeleteTagAsync(Guid id);
    }
}
