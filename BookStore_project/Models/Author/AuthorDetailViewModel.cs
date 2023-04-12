using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entity;
namespace BookStore_project.Models.Author
{
    public class AuthorDetailViewModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public DateTime DOB { get; set; }
        public string Img_url { get; set; }
        public List<Entity.Book>? lob { get; set; }
       
    }
    
}
