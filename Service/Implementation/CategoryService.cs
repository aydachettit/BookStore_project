using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.implementation
{
    public class CategoryService : ICategoryService
    {
        private ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsSync(Category newCategory)
        {
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsSync(Category deleteCategory)
        {
            _context.Categories.Remove(deleteCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByID(int id)
        {
            var model = GetByID(id);
            _context.Categories.Remove(model);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public List<Book> GetBookByCategoryID(int id)
        {
            return _context.Books.Where(c => c.CategoryID == id).ToList();
        }

        public Category GetByID(int id)
        {
            return _context.Categories.Where(c => c.ID == id).FirstOrDefault();

        }

        public async Task UpdateAsSync(Category updateCategory)
        {
            _context.Categories.Update(updateCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateByID(int id)
        {
            var model = GetByID(id);
            _context.Categories.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
