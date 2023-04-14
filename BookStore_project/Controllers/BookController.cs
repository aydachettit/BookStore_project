using BookStore_project.Models.Author;
using BookStore_project.Models.Book;
using DataAccess;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Service;
using Service.Implementation;
using System.Text.Json;

namespace BookStore_project.Controllers
{
    public class BookController : Controller
    {
        private IBookService _bookService;
        private IAuthorService _authorService;
        private ICategoryService _categoryService;
        private IPublisherService _publisherService;
        private IWebHostEnvironment _hostingEnvironment;
        private ApplicationDbContext _context;

        public BookController(IBookService bookService, ICategoryService categoryService, IPublisherService publisherService, IAuthorService authorService, ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _bookService = bookService;
            _authorService = authorService;
            _publisherService = publisherService;
            _categoryService = categoryService;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public List<BookIndexViewModel> Index()
        {
            //var listCategory = _categoryService.GetAll().Select(c => c.CategoryID).ToList();


            var model = _bookService.GetAll().Select(c => new BookIndexViewModel
            {
                ID = c.ID,
                Name = c.Name,
                PublicDate = c.PublicDate,
                Amount = c.Amount,
                Price = c.Price,
                Image_URL= c.Image_URL,

            }).ToList();
            return model;
        }
    }
}
