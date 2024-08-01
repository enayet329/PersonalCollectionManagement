

using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;

namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface ILikeService 
    {
        Task<int> GetAllLikeByItemId(Guid id);
        Task<OperationResult> ToggleLike(LikeRequestDto requestDto);
    }
}
