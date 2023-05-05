using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookStore_project.Models.Employee
{
    public class EmployeeEditViewModel
    {
        public int employeeID { get; set; }
        [Required(ErrorMessage = "Name is required"), StringLength(50, MinimumLength = 2)]
        public string? employeeName { get; set; }
        public string? employeeGender { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Joined")]

        public DateTime employeeDate_Join { get; set; }
        [Display(Name = " Phone Number")]

        public int? employeePhone_Number { get; set; }
        [Display(Name = " Employee Image")]

        public IFormFile? employeeImage { get; set; }
        public string? employeeAddress { get; set; }
        [Display(Name = " Date Of Birth")]

        public DateTime employeeDOB { get; set; }
        [Display(Name = " Email")]

        public string? employeeEmail { get; set; }
    }
}
