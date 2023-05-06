using BookStore_project.Models.Publisher;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;
using Service;
using Service.implementation;
using Service.Implementation;

namespace BookStore_project.Controllers
{
    public class PublisherController : Controller
    {
        private IPublisherService _publisherService;
        private ICategoryService _categoryService;
        private IWebHostEnvironment _hostingEnvironment;
        public PublisherController(ICategoryService categoryService,IPublisherService publisherservice, IWebHostEnvironment hostingEnvironment)
        {
            _categoryService = categoryService;
            _publisherService = publisherservice;
            _hostingEnvironment = hostingEnvironment;
        }
       
        public IActionResult Index(int? page)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var model = _publisherService.GetAll().Select(Publisher => new PublisherIndexViewModel
            {
                ID = Publisher.ID,
                Name = Publisher.Name,
                Country=Publisher.Country
            }).OrderBy(x=>x.ID).ToList();
            int pagesize = 5;
            int pagenumber = (page ?? 1);

            return View(model.ToPagedList(pagenumber, pagesize));
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
            var publisher = _publisherService.GetById(id);
            var model = new PublisherDetailViewModel();
            model.ID = publisher.ID;
            model.Name = publisher.Name;
            model.Country = publisher.Country;
            var publisherbook = _publisherService.getBookByPublisherId(id);
            model.lob = publisherbook;
            foreach (var items in model.lob)
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
            var publisher = _publisherService.GetById(id);
            var model = new PublisherDeleteViewModel();
            model.ID = publisher.ID;
            model.Name = publisher.Name;
            model.Country = publisher.Country;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Delete(PublisherDeleteViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var publisher = _publisherService.GetById(model.ID);
                await _publisherService.DeleteAsSync(publisher);
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
            var model = new PublisherCreateViewModel();
            IEnumerable<string> countries = new List<string> { "USA", "Canada", "Mexico","Viet Nam","Campuchia","China" };
            IEnumerable<SelectListItem> countryListItems = countries.Select(c => new SelectListItem
            {
                Value = c,
                Text = c
            }).ToList();
            model.Country = countryListItems;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(PublisherCreateViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var publisher = new Publisher
                {
                    ID = model.ID,
                    Name = model.Name,
                    Country=model.CountryName

                };
                await _publisherService.CreateAsSync(publisher);
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
            var publisher = _publisherService.GetById(id);
            var model = new PublisherEditViewModel();
            model.ID = publisher.ID;
            model.Name = publisher.Name;
            IEnumerable<string> countries = new List<string> { "USA", "Canada", "Mexico", "Viet Nam", "Campuchia", "China" };
            IEnumerable<SelectListItem> countryListItems = countries.Select(c => new SelectListItem
            {
                Value = c,
                Text = c
            }).ToList();
            model.Country = countryListItems;
            model.CountryName = publisher.Country;

            ViewBag.Country = model.Country;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(PublisherEditViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var publisher = new Publisher
                {
                    ID = model.ID,
                    Name = model.Name,
                    Country=model.CountryName
                };
                await _publisherService.UpdateAsSync(publisher);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
