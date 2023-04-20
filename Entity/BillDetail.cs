using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BillDetail
    {
        [Key]
        public int Bill_Detail_ID { get; set; }
        [ForeignKey("Book")]

        public int Book_ID { get; set; }
        [ForeignKey("Bill")]
        public int Bill_ID { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }

    }
}
