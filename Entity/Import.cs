using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Import
    {
        [Key]
        public int id { get; set; }
        public DateTime date_import { get; set; }

        public double Total { get; set; }

    }
}
