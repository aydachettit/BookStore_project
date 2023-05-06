using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore_project.Models.Shipment
{
    public class ShipmentEditViewModel
    {
        public int ID { get; set; }
        public int BillID { get; set; }
        public IEnumerable<SelectListItem> Bills;
        public string CustomerID { get; set; }
        public IEnumerable<SelectListItem> Customers;
        public int Shipment_Status_ID { get; set; }
        public IEnumerable<SelectListItem> Status;

    }
}
