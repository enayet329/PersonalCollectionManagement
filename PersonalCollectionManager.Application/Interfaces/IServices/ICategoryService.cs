using PersonalCollectionManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCollectionManager.Application.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<bool> AddCategory(Category category);
        Task<bool> RemoveCategory(Category category);
    }
}
