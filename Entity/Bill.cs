using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity {
    public class Bill {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int Total_money { get; set; }

        [ForeignKey("Customer")]
        public int Customer_ID { get; set; }
        [ForeignKey("Employee")]
        public int Employee_ID { get; set; }
        [ForeignKey("Status")]
        public int Bill_status_ID { get; set; }
    }
}