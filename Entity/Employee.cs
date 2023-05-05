using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Employee
    {
        public int employeeID { get; set; }
        public string? employeeName { get; set; }
        public string? employeeGender { get; set; }
        public DateTime employeeDate_Join { get; set; }
        public int? employeePhone_Number { get; set; }
        public string employeeImage { get; set; }
        public string employeeAddress { get; set; }
        public DateTime employeeDOB { get; set; }
        public string? employeeEmail { get; set; }

    }
}
