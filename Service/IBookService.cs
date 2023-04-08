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
        Task UpdateByID(int id);
        Task DeleteByID(int id);
        Book GetByID(int id);
        IEnumerable<Book> GetAll();

    }
}
