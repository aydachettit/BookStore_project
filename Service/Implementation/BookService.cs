using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.implementation
{
    public class BookService : IBookService
    {
        private ApplicationDbContext _context;
        public BookService(ApplicationDbContext context)
        {
            _context = context;
        } 
        public async Task CreateAsSync(Book newBook)
        {
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsSync(Book deleteBook)
        {
            _context.Books.Update(deleteBook);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByID(int id)
        {
            var model = GetByID(id);
            _context.Books.Remove(model);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public List<Book> getBookByAuthorID(int id)
        {
            return _context.Books.Where(c => c.AuthorID == id).ToList();
        }

        public Book GetByID(int id)
        {
            return _context.Books.Where(c => c.ID == id).FirstOrDefault();
        }

        public async Task UpdateAsSync(Book updateBook)
        {
            _context.Books.Update(updateBook);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateByID(int id)
        {
            var model = GetByID(id);
            _context.Books.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
