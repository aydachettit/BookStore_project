using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore_project.Models.Category
{
    public class CategoryDetailViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Entity.Book>? books { get; set; }
    }
}
