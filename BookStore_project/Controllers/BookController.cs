using BookStore_project.Models.Author;
using BookStore_project.Models.Book;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Service;
using Service.Implementation;

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

        public BookController(IBookService bookService,ICategoryService categoryService, IPublisherService publisherService, IAuthorService authorService, IWebHostEnvironment hostingEnvironment)
        {
            _bookService = bookService;
            _authorService = authorService;
            _publisherService = publisherService;
            _categoryService = categoryService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            //var listCategory = _categoryService.GetAll().Select(c => c.CategoryID).ToList();

            var model = _bookService.GetAll().Select(book => new BookIndexViewModel
            {
                BookID = book.BookID,
                BookName = book.BookName,
                PublicDate = book.PublicDate,
                Amount = book.Amount,
                Price = book.Price,
                Image_URL = book.Image_URL,
                AuthorID = book.BookAuthorID,
                CategoryID = book.BookCategoryID,
                PublisherID = book.PublisherID
            }).ToList();


            return View(model);
        }
    }
}
