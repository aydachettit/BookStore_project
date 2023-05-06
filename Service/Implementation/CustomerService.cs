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
        public async Task CreateAsAsync(Customer newCustomer)
        {
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsAsync(Customer deleteCustomer)
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

        public IEnumerable<Customer> GetCustomerByAddress(string address)
        {
            return _context.Customers.Where(x => x.Address == address).ToList();
        }

        public IEnumerable<Customer> GetCustomerByGender(string gender)
        {
            return _context.Customers.Where(x => x.Gender == gender).ToList();
        }

        public IEnumerable<Customer> GetCustomerByName(string name)
        {
            return _context.Customers.Where(x => x.Name == name).ToList();
        }

        public IEnumerable<Customer> GetCustomerByPhone(string phone)
        {
            return _context.Customers.Where(x => x.Phone == phone).ToList();
        }

        public async Task UpdateAsAsync(Customer updateCustomer)
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
