using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Shipment
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Bill")]
        public int BillID { get; set; }
        public string CustomerID { get; set;}
        public string CustomerName { get; set; }
        public int Shipment_Status_ID { get; set;}
    }
}
