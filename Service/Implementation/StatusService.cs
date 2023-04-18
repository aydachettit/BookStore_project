using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.implementation {
    public class StatusService : IStatusService {
        public ApplicationDbContext _context;
        public StatusService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task CreateAsSync(Status newStatus)
        {
            _context.Status.Add(newStatus);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsSync(Status deleteStatus)
        {
            _context.Status.Update(deleteStatus);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByID(int id)
        {
            var model = GetByID(id);
            _context.Status.Remove(model);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Status> GetAll()
        {
            return _context.Status.ToList();
        }

        public Status GetByID(int id)
        {
            return _context.Status.Where(x => x.ID == id).FirstOrDefault();
        }

        public async Task UpdateAsSync(Status updateStatus)
        {
            _context.Status.Update(updateStatus);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateByID(int id)
        {
            var status = GetByID(id);
            _context.Status.Update(status);
            await _context.SaveChangesAsync();
        }
    }
}