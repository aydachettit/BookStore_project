﻿using System;
using System.Collections.Generic;
using Entity;
using Service;
using DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;

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

       
        public List<Book> getBookByAuthorID(int id) { 
            return _context.Books.Where(c => c.AuthorID == id).ToList(); 
        }

        public List<Author> getAuthorbyName(string name)
        {
            return _context.Authors.Where(x => x.Name.Contains(name)).ToList();
        }
    }
}
