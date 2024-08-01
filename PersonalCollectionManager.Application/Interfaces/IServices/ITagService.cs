using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;

namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface ITagService 
    {
        Task<IEnumerable<TagDTO>> GetAllTagAsync();
        Task<TagDTO> GetTagByIdAsync(Guid id);
        Task<OperationResult> AddTagAsync(TagRequestDto tag);
        Task<OperationResult> UpdateTagAsync(TagRequestDto tag);
        Task<OperationResult> DeleteTagAsync(Guid id);
    }
}
