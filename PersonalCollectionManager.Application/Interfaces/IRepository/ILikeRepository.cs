using PersonalCollectionManager.Application.DTOs.RequestDtos;
using PersonalCollectionManager.Application.DTOs.ResponseDtos;
using PersonalCollectionManager.Domain.Entities;


namespace PersonalCollectionManager.Application.Interfaces.IRepository
{
    public interface ILikeRepository : IRepository<Like>
    {
        Task<Like> GetLikeByUserIdAndItemId(LikeRequestDto requestDto);
        Task<int> GetLikesByItemId(Guid itemId);
    }
}
