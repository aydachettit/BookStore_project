using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IEmployeeService
    {
        Task CreateAsAsync(Employee new_employee);
        Task UpdateAsAsync(Employee update_employee);
        Task DeleteAsAsync(Employee delete_employee);
        Task UpdateByID(int id);
        Task DeleteByID(int id);
        Employee GetByID(int id);
        IEnumerable<Employee> GetAll();
    }
}
