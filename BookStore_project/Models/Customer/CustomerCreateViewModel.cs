using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookStore_project.Models.Customer
{
    public class CustomerCreateViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Customer Name is required"), StringLength(100, MinimumLength = 2)]
        [RegularExpression(@"^[A-Za-zÀ-ỹ0-9""'\s-]*$"), Display(Name = "Customer Name")]
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
    }
}
