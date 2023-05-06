namespace BookStore_project.Models.Shipment
{
    public class ShipmentIndexViewModel
    {
        public int ID { get; set; }
        public int BillID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int Shipment_Status_ID { get; set; }
        public string StatusName { get; set; }
    }
}
