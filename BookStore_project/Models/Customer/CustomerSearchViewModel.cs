using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookStore_project.Models.Customer
{
    public class CustomerSearchViewModel
    {
        [Display(Name = "Search by")]
        public int SearchKeyID { get; set; }

        [Display(Name = "Key word")]
        public string Keyword { get; set; }
    }
}
