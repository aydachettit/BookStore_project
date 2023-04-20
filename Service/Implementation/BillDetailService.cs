using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class BillDetailService : IBillDetailService
    {
        private ApplicationDbContext _context;
        public BillDetailService(ApplicationDbContext context)
        {
            _context= context;
        }

        public async Task CreateAsAsync(BillDetail new_BillDetail)
        {
            _context.BillDetail.Add(new_BillDetail);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsAsync(BillDetail delete_BillDetail)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BillDetail> GetAll()
        {
            return _context.BillDetail.ToList();
        }

        public BillDetail GetByID(int id)
        {
            return _context.BillDetail.Where(x => x.Bill_Detail_ID == id).FirstOrDefault();
        }

        public Task UpdateAsAsync(BillDetail update_BillDetail)
        {
            throw new NotImplementedException();
        }

        public Task UpdateByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
