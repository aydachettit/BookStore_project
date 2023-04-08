using System;
using System.Collections.Generic;
using Entity;
using Service;
using DataAccess;

namespace Service.Implementation
{
    public class AuthorService:IAuthorService
    {
        /// <summary>
        /// Missing DataAccess
        /// </summary>
        private ApplicationDbContext _context;
        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsSync(Author AutoToadd)
        {
            _context.Authors.Add(AutoToadd);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsSync(Author AuthorTodelete)
        {
            _context.Authors.Remove(AuthorTodelete);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var author = GetById(id);
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public Task EditById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Author> GetAll()
        {
            return _context.Authors.ToList();
        }

        public Author GetById(int id)
        {

            return _context.Authors.Where(x => x.ID == id).FirstOrDefault();
        }

        public async Task UpdateAsSync(Author newAuthor)
        {
            _context.Authors.Update(newAuthor);
            await _context.SaveChangesAsync();
        }

        public Task UpdateById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
