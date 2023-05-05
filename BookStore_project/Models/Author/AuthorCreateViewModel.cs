using System.ComponentModel.DataAnnotations;

namespace BookStore_project.Models.Author
{
    public class AuthorCreateViewModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public DateTime DOB { get; set; }
        public IFormFile Image_URL { get; set; }
    }
}
