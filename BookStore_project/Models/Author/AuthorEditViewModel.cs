using System.ComponentModel.DataAnnotations;

namespace BookStore_project.Models.Author
{
    public class AuthorEditViewModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public DateTime DOB { get; set; }
        public string? Img_url { get; set; }
    }
}
