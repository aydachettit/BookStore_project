using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IPublisherService
    {
        Task CreateAsSync(Publisher Publisher);
        Task DeleteAsSync(Publisher Publisher);
        Task UpdateAsSync(Publisher Publisher);
        Task DeleteById(int id);
        Publisher GetById(int id);
        IEnumerable<Publisher> GetAll();

        List<Book> getBookByPublisherId(int id);
    }
}
