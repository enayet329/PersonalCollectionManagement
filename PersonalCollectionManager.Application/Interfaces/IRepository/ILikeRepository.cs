using PersonalCollectionManager.Domain.Entities;


namespace PersonalCollectionManager.Application.Interfaces.IRepository
{
    public interface ILikeRepository : IRepository<Like>
    {
        Task<IEnumerable<Like>> GetLikeByUserAndItemAsync(Guid itemId);
    }
}
