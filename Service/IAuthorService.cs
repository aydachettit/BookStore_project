using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Entity;
namespace Service
{
    public interface IAuthorService
    {
        Task CreateAsSync(Author AutoToadd);
        Task DeleteAsSync(Author AuthorTodelete);
        Task UpdateById(int id);
        Task EditById(int id);
        Task UpdateAsSync(Author newAuthor);
        Task DeleteById(int id);
        Author GetById(int id);
        IEnumerable<Author> GetAll();
    }
}
