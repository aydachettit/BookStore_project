using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore_project.Models.Publisher
{
    public class PublisherEditViewModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? CountryName { get; set; }
        public IEnumerable<SelectListItem>? Country { get; set; }
    }
}
