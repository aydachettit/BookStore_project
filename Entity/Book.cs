using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    
    public class Book
    {
        [Key]
        public int ID {get; set; }
        public string Name { get; set; }
        public DateTime PublicDate { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }

        public string? Description { get; set; }

        public string? Image_URL { get; set; }

        [ForeignKey("Author")]
        public int AuthorID { get; set; }
        public Author Author { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherID { get; set; }
        public Publisher Publisher { get; set; }
        [ForeignKey("Category")]
        public int CategoryID {get; set; }
        public Category Category { get; set; }

    }
}
