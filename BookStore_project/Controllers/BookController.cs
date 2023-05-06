
using BookStore_project.Models.Book;
using DataAccess;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;
using Service;
using System.Data;


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
        
        public IActionResult Index(int? page)
        {

            var model = _bookService.GetAll().Select(c => new BookIndexViewModel
            {
                ID = c.ID,
                Name = c.Name,
                PublicDate = c.PublicDate,
                Amount = c.Amount,
                Price = c.Price,
                Description = c.Description,
                Image_URL = c.Image_URL,
                AuthorID = c.AuthorID,
                PublisherID = c.PublisherID,
                CategoryID = c.CategoryID,
            }).OrderBy(x => x.ID).ToList();
            int pagesize = 5;
            int pagenumber = (page ?? 1);

            foreach (var item in model)
            {
                item.Author = _authorService.GetById(item.AuthorID).Name;
                item.Category = _categoryService.GetByID(item.CategoryID).Name;
                item.Publisher = _publisherService.GetById(item.PublisherID).Name;
            }

            return View(model.ToPagedList(pagenumber, pagesize));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            var model = new BookCreateViewModel();

            IEnumerable<SelectListItem> authorList = _authorService.GetAll().
                Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name,
                }).ToList();
            IEnumerable<SelectListItem> categoryList = _categoryService.GetAll().
                Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name,
                }).ToList();

            IEnumerable<SelectListItem> publisherList = _publisherService.GetAll().
                Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name,
                }).ToList();

            model.Authors = authorList;
            model.Categories = categoryList;
            model.Publishers = publisherList;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

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
                    Description = model.Description,
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

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult Edit(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var book = _bookService.GetByID(id);
            var model = new BookEditViewModel();
            model.ID = book.ID;
            model.Name = book.Name;
            model.PublicDate = book.PublicDate;
            model.Amount = book.Amount;
            model.Price = book.Price;
            model.Description = book.Description;

            IEnumerable<SelectListItem> authorList = _authorService.GetAll().
                Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name,
                }).ToList();
            IEnumerable<SelectListItem> categoryList = _categoryService.GetAll().
                Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name,
                }).ToList();

            IEnumerable<SelectListItem> publisherList = _publisherService.GetAll().
                Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name,
                }).ToList();

            model.Authors = authorList;
            model.Categories = categoryList;
            model.Publishers = publisherList;


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(BookEditViewModel model)
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
                    Description = model.Description,
                    AuthorID = model.AuthorID,
                    CategoryID = model.CategoryID,
                    PublisherID = model.PublisherID,
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
                await _bookService.UpdateAsSync(book);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult Delete(int id)
        {
            var book = _bookService.GetByID(id);
            if (book == null)
            {
                return NotFound();
            }
            var model = new BookDeleteViewModel
            {
                ID = book.ID,
                Name = book.Name,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(BookDeleteViewModel model)
        {
            var book = _bookService.GetByID(model.ID);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.DeleteByID(model.ID);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult Detail(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var book = _bookService.GetByID(id);
            var authorName = _authorService.GetById(book.AuthorID).Name;
            var categoryName = _categoryService.GetByID(book.CategoryID).Name;
            var publisherName = _publisherService.GetById(book.PublisherID).Name;

            var model = new BookDetailViewModel();
            model.ID = book.ID;
            model.Name = book.Name;
            model.PublicDate = book.PublicDate;
            model.Image_URL = book.Image_URL;
            model.Amount = book.Amount;
            model.Price = book.Price;
            model.Description = book.Description;
            model.Authors = authorName;
            model.Categories = categoryName;
            model.Publishers = publisherName;


            return View(model);
        }

        [HttpGet]
        public IActionResult SearchPage()
        {
            var model = new BookSearchViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult SearchResultPage(BookSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                int keyID = model.SearchKeyID;
                string keyword = model.Keyword;

                switch (keyID)
                {
                    case 1:
                        var bookModel = _bookService.GetBookByName(keyword).Select(book => new BookIndexViewModel
                        {
                            ID = book.ID,
                            Name = book.Name,
                            PublicDate = book.PublicDate,
                            Amount = book.Amount,
                            Price = book.Price,
                            Image_URL = book.Image_URL,
                            Description = book.Description,
                            AuthorID = book.AuthorID,
                            CategoryID = book.CategoryID,
                            PublisherID = book.PublisherID,
                        }).ToList();
                        foreach (var item in bookModel)
                        {
                            item.Author = _authorService.GetById(item.AuthorID).Name;
                            item.Category = _categoryService.GetByID(item.CategoryID).Name;
                            item.Publisher = _publisherService.GetById(item.PublisherID).Name;
                        }
                        return View(bookModel);

                    case 2:
                        bookModel = _bookService.GetBookByPublicDate(keyword).Select(book => new BookIndexViewModel
                        {
                            ID = book.ID,
                            Name = book.Name,
                            PublicDate = book.PublicDate,
                            Amount = book.Amount,
                            Price = book.Price,
                            Description = book.Description,
                            Image_URL = book.Image_URL,
                            AuthorID = book.AuthorID,
                            CategoryID = book.CategoryID,
                            PublisherID = book.PublisherID,
                        }).ToList();
                        foreach (var item in bookModel)
                        {
                            item.Author = _authorService.GetById(item.AuthorID).Name;
                            item.Category = _categoryService.GetByID(item.CategoryID).Name;
                            item.Publisher = _publisherService.GetById(item.PublisherID).Name;
                        }
                        return View(bookModel);

                    case 3:
                        bookModel = _bookService.GetBookByAmount(Int32.Parse(keyword)).Select(book => new BookIndexViewModel
                        {
                            ID = book.ID,
                            Name = book.Name,
                            PublicDate = book.PublicDate,
                            Amount = book.Amount,
                            Price = book.Price,
                            Description = book.Description,
                            Image_URL = book.Image_URL,
                            AuthorID = book.AuthorID,
                            CategoryID = book.CategoryID,
                            PublisherID = book.PublisherID,
                        }).ToList();
                        foreach (var item in bookModel)
                        {
                            item.Author = _authorService.GetById(item.AuthorID).Name;
                            item.Category = _categoryService.GetByID(item.CategoryID).Name;
                            item.Publisher = _publisherService.GetById(item.PublisherID).Name;
                        }
                        return View(bookModel);
                    case 4:
                        bookModel = _bookService.GetBookByPrice(Int32.Parse(keyword)).Select(book => new BookIndexViewModel
                        {
                            ID = book.ID,
                            Name = book.Name,
                            PublicDate = book.PublicDate,
                            Amount = book.Amount,
                            Price = book.Price,
                            Description = book.Description,
                            Image_URL = book.Image_URL,
                            AuthorID = book.AuthorID,
                            CategoryID = book.CategoryID,
                            PublisherID = book.PublisherID,
                        }).ToList();
                        foreach (var item in bookModel)
                        {
                            item.Author = _authorService.GetById(item.AuthorID).Name;
                            item.Category = _categoryService.GetByID(item.CategoryID).Name;
                            item.Publisher = _publisherService.GetById(item.PublisherID).Name;
                        }
                        return View(bookModel);
                    case 5:
                        bookModel = _bookService.GetBookByAuthor(keyword).Select(book => new BookIndexViewModel
                        {
                            ID = book.ID,
                            Name = book.Name,
                            PublicDate = book.PublicDate,
                            Amount = book.Amount,
                            Price = book.Price,
                            Description = book.Description,
                            Image_URL = book.Image_URL,
                            AuthorID = book.AuthorID,
                            CategoryID = book.CategoryID,
                            PublisherID = book.PublisherID,
                        }).ToList();
                        foreach (var item in bookModel)
                        {
                            item.Author = _authorService.GetById(item.AuthorID).Name;
                            item.Category = _categoryService.GetByID(item.CategoryID).Name;
                            item.Publisher = _publisherService.GetById(item.PublisherID).Name;
                        }
                        return View(bookModel);
                    case 6:
                        bookModel = _bookService.GetBookByCategory(keyword).Select(book => new BookIndexViewModel
                        {
                            ID = book.ID,
                            Name = book.Name,
                            PublicDate = book.PublicDate,
                            Amount = book.Amount,
                            Price = book.Price,
                            Description = book.Description,
                            Image_URL = book.Image_URL,
                            AuthorID = book.AuthorID,
                            CategoryID = book.CategoryID,
                            PublisherID = book.PublisherID,
                        }).ToList();
                        foreach (var item in bookModel)
                        {
                            item.Author = _authorService.GetById(item.AuthorID).Name;
                            item.Category = _categoryService.GetByID(item.CategoryID).Name;
                            item.Publisher = _publisherService.GetById(item.PublisherID).Name;
                        }
                        return View(bookModel);
                    case 7:
                        bookModel = _bookService.GetBookByPublisher(keyword).Select(book => new BookIndexViewModel
                        {
                            ID = book.ID,
                            Name = book.Name,
                            PublicDate = book.PublicDate,
                            Amount = book.Amount,
                            Price = book.Price,
                            Description = book.Description,
                            Image_URL = book.Image_URL,
                            AuthorID = book.AuthorID,
                            CategoryID = book.CategoryID,
                            PublisherID = book.PublisherID,
                        }).ToList();
                        foreach (var item in bookModel)
                        {
                            item.Author = _authorService.GetById(item.AuthorID).Name;
                            item.Category = _categoryService.GetByID(item.CategoryID).Name;
                            item.Publisher = _publisherService.GetById(item.PublisherID).Name;
                        }
                        return View(bookModel);
                }

            }
            return View();


        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [HttpGet]
        public async Task<IActionResult> CreateProductDetail(int ID , int authorID = 1)
        {

            //ViewData["CurrentFilter"] = prodIDuctID;
            var ProductDetail = _bookService.GetByID(ID);
            var author = _authorService.GetById(authorID);

            if (author == null || ProductDetail == null)
            {
                return NotFound();
            }
            string categories = _categoryService.GetByID(ProductDetail.CategoryID).Name;
            string publisher = _publisherService.GetById(ProductDetail.PublisherID).Name;
            var model = new CreateProductDetailViewModel
            {
                ID = ProductDetail.ID,
                Name = ProductDetail.Name,
                Amount = ProductDetail.Amount,
                Price = ProductDetail.Price,
                PublicDate = ProductDetail.PublicDate,
                Description = ProductDetail.Description,
                AuthorID = author.ID,
                Authors = author.Name,
                Image_URL = ProductDetail.Image_URL,
                CategoryID = ProductDetail.CategoryID,
                PublisherID = ProductDetail.PublisherID,
                //Categories = ProductDetail.Category.Name.ToString()
                Categories = categories,
                Publishers = publisher



            };


            return View(model);
        }
    }
}
