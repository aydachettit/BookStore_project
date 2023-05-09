using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore_project.Models.Bill
{
    public class BillDeleteViewModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int Total_money { get; set; }

        public string Customer_ID { get; set; }
        public int Employee_ID { get; set; }
        public int Bill_status_ID { get; set;}
    }
}