using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            _context.Books.Remove(deleteBook);
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

        public IEnumerable<Book> GetBookByAmount(int amount)
        {
            return _context.Books.Where(x => x.Amount == amount).ToList();
        }

        public IEnumerable<Book> GetBookByAuthor(string author)
        {
            return _context.Books.Where(x => x.Author.Name == author).ToList();
        }

        public IEnumerable<Book> GetBookByCategory(string category)
        {
            return _context.Books.Where(x => x.Category.Name == category).ToList();
        }

        public IEnumerable<Book> GetBookByName(string name)
        {
            return _context.Books.Where(x => x.Name == name).ToList();
        }

        public IEnumerable<Book> GetBookByPrice(int price)
        {
            return _context.Books.Where(x => x.Price == price).ToList();
        }

        public IEnumerable<Book> GetBookByPublicDate(string date)
        {
            DateTime enteredDate = DateTime.Parse(date);
            return _context.Books.Where(c => c.PublicDate == enteredDate).ToList();
        }

        public IEnumerable<Book> GetBookByPublisher(string publisher)
        {
            return _context.Books.Where(x => x.Publisher.Name == publisher).ToList();
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
