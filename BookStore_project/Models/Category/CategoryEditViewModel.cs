using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookStore_project.Models.Category
{
    public class CategoryEditViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Category Name is required"), StringLength(100, MinimumLength = 2)]
        [RegularExpression(@"^[A-Za-zÀ-ỹ0-9""'\s-]*$"), Display(Name = "Category Name")]
        public string Name { get; set; }
    }
}
