using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Publisher
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required ,MaxLength(50)]
        public string Country { get; set; }
    }
}
