using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IBillDetailService
    {
        Task CreateAsAsync(BillDetail new_BillDetail);
        Task DeleteAsAsync(BillDetail delete_BillDetail);
        Task UpdateAsAsync(BillDetail update_BillDetail);
        Task UpdateByID(int id);
        Task DeleteByID(int id);
        BillDetail GetByID(int id);
        IEnumerable<BillDetail> GetAll();
    }
}
