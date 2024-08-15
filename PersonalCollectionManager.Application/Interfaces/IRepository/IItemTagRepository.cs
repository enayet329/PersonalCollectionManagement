using PersonalCollectionManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Application.Interfaces.IRepository
{
    public interface IItemTagRepository : IRepository<ItemTag>
    {
        void AddTagToItem(Guid itemId, Guid tagId);
        void RemoveTagFromItem(Guid itemId, Guid tagId);
        Task<IEnumerable<ItemTag>> getTagsByItemAsync(Guid itemId);
    }
}
