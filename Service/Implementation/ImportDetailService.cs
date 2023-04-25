using DataAccess;
using Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ImportDetailService : IImportDetailService
    {
        private ApplicationDbContext _context;
        public ImportDetailService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsSync(ImportDetail detail)
        {
            _context.ImportDetails.Add(detail);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> getAllBookforImport()
        {
            var book_name = _context.Books.Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = b.ID.ToString()

            });
            return book_name;
        }

        public List<ImportDetail> getAllByImportId(int id)
        {
            return _context.ImportDetails.Where(x => x.import_id == id).ToList();
        }

        public Book searchBookByName(string title)
        {
            return _context.Books.Where(x => x.Name == title).FirstOrDefault();
        }

      
    }
}
