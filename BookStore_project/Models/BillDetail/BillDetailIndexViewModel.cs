using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore_project.Models.BillDetail
{
    public class BillDetailIndexViewModel
    {

        public int Bill_Detail_ID { get; set; }

        public int Amount { get; set; }
        public float Price { get; set; }
    }
}
