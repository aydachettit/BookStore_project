namespace BookStore_project.Models.Employee
{
    public class EmployeeDetailViewModel
    {
        public int employeeID { get; set; }
        public string? employeeName { get; set; }
        public string? employeeGender { get; set; }
        public int? employeeDate_Join { get; set; }
        public int? employeePhone_Number { get; }
    }
}
