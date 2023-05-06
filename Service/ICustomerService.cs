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
        Task CreateAsAsync(Customer newCustomer);
        Task UpdateAsAsync(Customer updateCustomer);
        Task DeleteAsAsync(Customer deleteCustomer);
        Task UpdateByID(int id);
        Task DeleteByID(int id);
        Customer GetByID(int id);
        IEnumerable<Customer> GetAll();

        IEnumerable<Customer> GetCustomerByName(string name);
        IEnumerable<Customer> GetCustomerByGender(string gender);
        IEnumerable<Customer> GetCustomerByPhone(string phone);
        IEnumerable<Customer> GetCustomerByAddress(string address);
    }
}
