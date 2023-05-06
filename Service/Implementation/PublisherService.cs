using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class PublisherService : IPublisherService
    {
        private ApplicationDbContext _context;
        public PublisherService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsSync(Publisher Publisher)
        {
            _context.Publishers.Add(Publisher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsSync(Publisher Publisher)
        {
            _context.Publishers.Remove(Publisher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var Publisher = GetById(id);
            _context.Publishers.Remove(Publisher);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Publisher> GetAll()
        {
            return _context.Publishers.ToList();
        }

        public List<Book> getBookByPublisherId(int id)
        {
            return _context.Books.Where(x => x.PublisherID == id).ToList();
        }

        public Publisher GetById(int id)
        {
            return _context.Publishers.Where(x => x.ID == id).FirstOrDefault();
        }

        public async Task UpdateAsSync(Publisher Publisher)
        {
            _context.Publishers.Update(Publisher);
            await _context.SaveChangesAsync();
        }
    }
}
