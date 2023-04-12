using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service {
    public interface IBillService {
        Task CreateAsSync(Bill newBill);
        Task UpdateAsSync(Bill updateBill);
        Task DeleteAsSync(Bill deleteBill);
        Task UpdateByID(int id);
        Task DeleteByID(int id);
        Bill GetByID(int id);
        IEnumerable<Bill> GetAll();
    }
}