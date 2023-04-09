using System.ComponentModel.DataAnnotations;

namespace BookStore_project.Models.Author
{
    public class AuthorDeleteViewModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public DateTime DOB { get; set; }
        public string? img_url { get; set; }
    }
}
