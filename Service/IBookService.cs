using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IBookService
    {
        Task CreateAsSync(Book newBook);
        Task UpdateAsSync(Book updateBook);
        Task DeleteAsSync(Book deleteBook);
        Task UpdateByID(int id);
        Task DeleteByID(int id);
        Book GetByID(int id);
        IEnumerable<Book> GetAll();

        IEnumerable<Book> GetBookByName(string name);
        IEnumerable<Book> GetBookByNameAndAuthor(string search);
        IEnumerable<Book> GetBookByPublicDate(string date);
        IEnumerable<Book> GetBookByAmount(int amount);
        IEnumerable<Book> GetBookByPrice(int price);
        IEnumerable<Book> GetBookByAuthor(string author);
        IEnumerable<Book> GetBookByCategory(string category);
        IEnumerable<Book> GetBookByPublisher(string publisher);
    }
}
