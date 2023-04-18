using BookStore_project.Models.Author;
using BookStore_project.Models.Book;
using DataAccess;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Index()
        {

            var model = _bookService.GetAll().Select(c => new BookIndexViewModel
            {
                ID = c.ID,
                Name = c.Name,
                PublicDate = c.PublicDate,
                Amount = c.Amount,
                Price = c.Price,
                Image_URL = c.Image_URL,
                AuthorID = c.AuthorID,
                PublisherID = c.PublisherID,
                CategoryID = c.CategoryID,
            }).ToList();

            foreach (var item in model)
            {
                item.Author = _authorService.GetById(item.AuthorID).Name;
                item.Category = _categoryService.GetByID(item.CategoryID).Name;
                item.Publisher = _publisherService.GetById(item.PublisherID).Name;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new BookCreateViewModel();

            List<SelectListItem> authorList = _authorService.GetAll().
                Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name,
                }).ToList();
            List<SelectListItem> categoryList = _categoryService.GetAll().
                Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name,
                }).ToList();

            List<SelectListItem> publisherList = _publisherService.GetAll().
                Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name,
                }).ToList();


            model.Author = authorList;
            model.Category = categoryList;
            model.Publisher = publisherList;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    ID = model.ID,
                    Name = model.Name,
                    PublicDate = model.PublicDate,
                    Amount = model.Amount,
                    Price = model.Price,
                    AuthorID = model.AuthorID,
                    CategoryID = model.CategoryID,
                    PublisherID = model.PublisherID
                };

                if (model.Image_URL != null && model.Image_URL.Length > 0)
                {
                    var uploadDir = @"img/books";
                    var fileName = Path.GetFileNameWithoutExtension(model.Image_URL.FileName);
                    var extension = Path.GetExtension(model.Image_URL.FileName);
                    var webrootPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webrootPath, uploadDir, fileName);
                    await model.Image_URL.CopyToAsync(new FileStream(path, FileMode.Create));
                    book.Image_URL = "/" + uploadDir + "/" + fileName;
                }
                await _bookService.CreateAsSync(book);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
