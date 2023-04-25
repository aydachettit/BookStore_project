using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Composition.Convention;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ImportDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Import")]
        public int import_id { get; set; }

        public string book_name { get; set; }
        public int book_amount { get; set; }

        public double book_price { get; set; }

    }
}
