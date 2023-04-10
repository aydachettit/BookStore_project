using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICategoryService
    {
        Task CreateAsSync(Category newCategory);

        Task UpdateAsSync(Category updateCategory);

        Task DeleteAsSync(Category deleteCategory);
        Task UpdateByID(int id);
        Task DeleteByID(int id);
        Category GetByID(int id);
        IEnumerable<Category> GetAll();
    }
}
