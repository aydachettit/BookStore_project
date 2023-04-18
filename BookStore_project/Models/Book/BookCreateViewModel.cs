using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookStore_project.Models.Book
{
    public class BookCreateViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Book Name is required"), StringLength(100, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "Book Name")]
        public string Name { get; set; }
        [DataType(DataType.Date), Display(Name = "Public Date")]
        public DateTime PublicDate { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        [Display(Name = "Book Image")]
        public IFormFile Image_URL { get; set; }

        public int AuthorID { get; set; }
        public IEnumerable<SelectListItem> Author { get; set; }

        public int PublisherID { get; set; }
        public IEnumerable<SelectListItem> Publisher { get; set; }
        public int CategoryID { get; set; }
        public IEnumerable<SelectListItem> Category { get; set; }
    }
}
