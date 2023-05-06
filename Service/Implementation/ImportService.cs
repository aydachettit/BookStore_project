using DataAccess;
using Entity;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ImportService : IImport
    {
        private ApplicationDbContext _context;
        public ImportService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsSync(Import import)
        {
            _context.Imports.Add(import);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsSync(Import import)
        {
            var list = _context.ImportDetails.Where(x => x.import_id == import.id).ToList();
            foreach(var item in list)
            {
                if (item.import_id == import.id)
                {
                    _context.ImportDetails.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
            _context.Imports.Remove(import);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Import> GetAll()
        {
            return _context.Imports.ToList();
        }

        public Import GetById(int id)
        {
            return _context.Imports.Where(x => x.id == id).FirstOrDefault();
        }

        public async Task UpdateAsSnc(Import import)
        {
            _context.Imports.Update(import);
            await _context.SaveChangesAsync();
        }
    }
}
