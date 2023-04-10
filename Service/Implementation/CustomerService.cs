using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        public ApplicationDbContext _context;
        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsSync(Customer newCustomer)
        {
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsSync(Customer deleteCustomer)
        {
            _context.Customers.Remove(deleteCustomer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByID(int id)
        {
            var customer = GetByID(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer GetByID(int id)
        {
            return _context.Customers.Where(x => x.ID == id).FirstOrDefault();
        }

        public async Task UpdateAsSync(Customer updateCustomer)
        {
            _context.Customers.Update(updateCustomer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateByID(int id)
        {
            var customer = GetByID(id);
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
