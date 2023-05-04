using BookStore_project.Models.Publisher;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;
using Service.Implementation;

namespace BookStore_project.Controllers
{
    public class PublisherController : Controller
    {
        private IPublisherService _publisherService;
        private IWebHostEnvironment _hostingEnvironment;
        public PublisherController(IPublisherService publisherservice, IWebHostEnvironment hostingEnvironment)
        {
            _publisherService = publisherservice;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var model = _publisherService.GetAll().Select(Publisher => new PublisherIndexViewModel
            {
                ID = Publisher.ID,
                Name = Publisher.Name,
                Country=Publisher.Country
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var publisher = _publisherService.GetById(id);
            var model = new PublisherDetailViewModel();
            model.ID = publisher.ID;
            model.Name = publisher.Name;
            model.Country = publisher.Country;
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
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
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var publisher = _publisherService.GetById(id);
            var model = new PublisherEditViewModel();
            model.ID = publisher.ID;
            model.Name = publisher.Name;
            model.Country = publisher.Country;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PublisherEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var publisher = new Publisher
                {
                    ID = model.ID,
                    Name = model.Name,
                    Country=model.Country
                };
                await _publisherService.UpdateAsSync(publisher);
                RedirectToAction("Index");
            }
            return View();
        }
    }
}
