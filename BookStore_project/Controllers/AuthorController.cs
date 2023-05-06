using BookStore_project.Models.Author;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList;
using Service;
using Service.implementation;
using Service.Implementation;

namespace BookStore_project.Controllers
{
    public class AuthorController : Controller
    {
        private IAuthorService _authorService;
        private ICategoryService _categoryService;
        private IBookService _bookService;
        private IWebHostEnvironment _hostingEnvironment;
        public AuthorController(IAuthorService authorService, IBookService bookService, ICategoryService categoryService, IWebHostEnvironment hostingEnvironment)
        {
            _authorService = authorService;
            _bookService = bookService;
            _categoryService = categoryService;
            _hostingEnvironment = hostingEnvironment;
        }
        
        public IActionResult Index(int? page)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index","Home");
            }
            var model = _authorService.GetAll().Select(Author => new AuthorIndexViewModel
            {
                ID = Author.ID,
                Name = Author.Name,
                DOB = Author.DOB,
                img_url = Author.img_url
            }).OrderBy(x=>x.ID).ToList();
            int pagesize = 1;
            int pagenumber = (page ?? 1);

            return View(model.ToPagedList(pagenumber,pagesize));
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var author = _authorService.GetById(id);
            var author_book = _authorService.getBookByAuthorID(id);
            var model = new AuthorDetailViewModel();
            model.ID = author.ID;
            model.Name = author.Name;
            model.DOB = author.DOB;
            model.Img_url = author.img_url;
            model.lob = author_book;
            foreach(var items in model.lob)
            {
                var cate = _categoryService.GetByID(items.CategoryID);
                items.Category = cate;
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var author = _authorService.GetById(id);
            var model = new AuthorDeleteViewModel();
            model.ID = author.ID;
            model.Name = author.Name;
            model.DOB = author.DOB;
            model.img_url = author.img_url;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AuthorDeleteViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var author = _authorService.GetById(model.ID);
                await _authorService.DeleteAsSync(author);
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new AuthorCreateViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorCreateViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var author = new Author
                {
                    ID = model.ID,
                    Name = model.Name,
                    DOB = model.DOB

                };
                if (model.Image_URL != null && model.Image_URL.Length > 0)
                {
                    var uploadDir = @"img/authors";
                    var fileName = Path.GetFileNameWithoutExtension(model.Image_URL.FileName);
                    var extension = Path.GetExtension(model.Image_URL.FileName);
                    var webrootPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webrootPath, uploadDir, fileName);
                    await model.Image_URL.CopyToAsync(new FileStream(path, FileMode.Create));
                    author.img_url= "/" + uploadDir + "/" + fileName;
                }
                await _authorService.CreateAsSync(author);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var author = _authorService.GetById(id);
            var model = new AuthorEditViewModel();
            model.ID = author.ID;
            model.Name = author.Name;
            model.DOB = author.DOB;
            model.Img_url = author.img_url;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AuthorEditViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var author = new Author
                {
                    ID = model.ID,
                    Name = model.Name,
                    DOB = model.DOB,
                    img_url = model.Img_url
                };
                await _authorService.UpdateAsSync(author);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult SearchPage()
        {
            var model = new AuthorSearchViewModel();
            return View(model);
        }
        public IActionResult SearchByAuthorName(string name)
        {
            var model = _authorService.getAuthorbyName(name).Select(Author => new AuthorIndexViewModel
            {
                ID = Author.ID,
                Name = Author.Name,
                DOB = Author.DOB,
                img_url = Author.img_url
            }).ToList();
            return View(model);

        }
    }
}
