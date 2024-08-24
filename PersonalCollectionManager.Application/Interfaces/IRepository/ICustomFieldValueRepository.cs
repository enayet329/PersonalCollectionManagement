using PersonalCollectionManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Application.Interfaces.IRepository
{
    public interface ICustomFieldValueRepository : IRepository<CustomFieldValue>
    {
        Task<IEnumerable<CustomFieldValue>> GetByItemIdAsync(Guid itemId);
        Task<bool> UpdateCustomFieldValueAsync(Guid itemId, IEnumerable<CustomFieldValue> customFieldValues);
    }
}
