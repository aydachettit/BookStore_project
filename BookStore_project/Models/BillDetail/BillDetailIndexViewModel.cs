using BookStore_project.Models.Bill;
using BookStore_project.Models.Book;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore_project.Models.BillDetail
{
    public class BillDetailIndexViewModel
    {

        public int Bill_Detail_ID { get; set; }

        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public int Book_ID { get; set; }
        public int Bill_ID { get; set; }
       
    }
}
