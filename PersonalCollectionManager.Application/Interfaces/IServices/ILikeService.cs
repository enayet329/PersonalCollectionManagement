using PersonalCollectionManager.Application.Interfaces.IRepository;
using PersonalCollectionManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface ILikeService 
    {
        Task<IEnumerable<Like>> GetAllLike();
        Task<bool> AddLike(int userId, int collectionId);
        Task<bool> RemoveLike(int userId, int collectionId);
    }
}
