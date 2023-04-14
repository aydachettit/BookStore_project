using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICustomerService
    {
        Task CreateAsSync(Customer newCustomer);
        Task UpdateAsSync(Customer updateCustomer);
        Task DeleteAsSync(Customer deleteCustomer);
        Task UpdateByID(int id);
        Task DeleteByID(int id);
        Customer GetByID(int id);
        IEnumerable<Customer> GetAll();
    }
}
