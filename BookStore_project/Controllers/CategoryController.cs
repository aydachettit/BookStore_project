
using BookStore_project.Models.Author;
using BookStore_project.Models.Category;
using Entity;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using Service;


namespace BookStore_project.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        private IPublisherService _publisherService;
        private IAuthorService _authorService;
        private IWebHostEnvironment _hostingEnvironment;
        public CategoryController(ICategoryService categoryService, IPublisherService publisherService, IAuthorService authorService, IWebHostEnvironment hostingEnvironment)
        {
            _categoryService = categoryService;
            _publisherService = publisherService;
            _authorService = authorService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(int? page)
        {

            var model = _categoryService.GetAll().Select(c => new CategoryIndexViewModel
            {
                ID = c.ID,
                Name = c.Name,
            }).OrderBy(x => x.ID).ToList();

            int pagesize = 5;
            int pagenumber = (page ?? 1);

            return View(model.ToPagedList(pagenumber, pagesize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CategoryCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                var category = new Category
                {
                    ID = model.ID,
                    Name = model.Name,
                };
                await _categoryService.CreateAsSync(category);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var category = _categoryService.GetByID(id);
            var model = new CategoryEditViewModel();
            model.ID = category.ID;
            model.Name = category.Name;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    ID = model.ID,
                    Name = model.Name,
                };
           
                await _categoryService.UpdateAsSync(category);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetByID(id);
            if (category == null)
            {
                return NotFound();
            }
            var model = new CategoryDeleteViewModel()
            {
                ID = category.ID,
                Name = category.Name,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CategoryDeleteViewModel model)
        {
            var category = _categoryService.GetByID(model.ID);
            if (category == null)
            {
                return NotFound();
            }
            _categoryService.DeleteByID(model.ID);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var category = _categoryService.GetByID(id);
            var category_book = _categoryService.GetBookByCategoryID(id);
            var model = new CategoryDetailViewModel();
            model.ID = category.ID;
            model.Name = category.Name;
            model.books = category_book;

            foreach (var items in model.books)
            {
                var author = _authorService.GetById(items.AuthorID);
                var publisher = _publisherService.GetById(items.PublisherID);
                items.Author = author;
                items.Publisher = publisher;
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SearchPage()
        {
            var model = new CategorySearchViewModel();
            return View(model);
        }

        public IActionResult SearchByCategoryName(string name)
        {
            var model = _categoryService.GetCategoryByName(name).Select(category => new CategoryIndexViewModel
            {
                ID = category.ID,
                Name = category.Name
            }).ToList();
            return View(model);

        }
    }
}
