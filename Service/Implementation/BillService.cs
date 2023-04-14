using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.implementation {
    public class BillService : IBillService {
        public ApplicationDbContext _context;
        public BillService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task CreateAsSync(Bill newBill)
        {
            _context.Bills.Add(newBill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsSync(Bill deleteBill)
        {
            _context.Bills.Update(deleteBill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByID(int id)
        {
            var model = GetByID(id);
            _context.Bills.Remove(model);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Bill> GetAll()
        {
            return _context.Bills.ToList();
        }

        public Bill GetByID(int id)
        {
            return _context.Bills.Where(x => x.ID == id).FirstOrDefault();
        }

        public async Task UpdateAsSync(Bill updateBill)
        {
            _context.Bills.Update(updateBill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateByID(int id)
        {
            var bill = GetByID(id);
            _context.Bills.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}