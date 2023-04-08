using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Book
    {
        public int BookID {get; set; }
        public string BookName { get; set; }
        public DateOnly PublicDate { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public int BookAuthorID { get; set; }
        public int PublisherID { get; set; }
        public int BookCategoryID {get; set; }

    }
}
