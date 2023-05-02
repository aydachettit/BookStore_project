using BookStore_project.Models.Author;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_project.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
