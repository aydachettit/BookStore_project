using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        public ApplicationDbContext _context;
        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }


        
        public async Task CreateAsAsync(Employee new_employee)
        {
            _context.Employee.Add(new_employee);
                await _context.SaveChangesAsync();
        }

        public async Task DeleteAsAsync(Employee delete_employee)
        {
            var deleteEmployee = GetByID(delete_employee.employeeID);
            _context.Remove(deleteEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByID(int id)
        {
            var employee = GetByID(id);
            _context.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employee.ToList();
        }

        

        public async Task UpdateAsAsync(Employee update_employee)
        {
            _context.Employee.Update(update_employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateByID(int id)
        {
            var employee = GetByID(id);
            if (employee != null)
            {
                _context.Employee.Update(employee);
                await _context.SaveChangesAsync();
            }
        }

        

        public Employee GetByID(int id)
        {
            return _context.Employee.Where(x => x.employeeID == id).FirstOrDefault();
        }
    }
}
