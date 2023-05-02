using BookStore_project.Models.Author;
using BookStore_project.Models.Status;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.implementation;

namespace BookStore_project.Controllers
{
    public class StatusController : Controller
    {
        private IStatusService _statusService;
        private IWebHostEnvironment _hostingEnvironment;
        public StatusController(IStatusService statusService, IWebHostEnvironment hostingEnvironment)
        {
            _statusService = statusService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var model = _statusService.GetAll().Select(Status => new StatusIndexViewModel
            {
                ID = Status.ID,
                Name = Status.Name
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
            var status = _statusService.GetByID(id);
            var model = new StatusDetailViewModel();
            model.ID = status.ID;
            model.Name = status.Name;
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var status = _statusService.GetByID(id);
            var model = new StatusDeleteViewModel();
            model.ID = status.ID;
            model.Name = status.Name;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(StatusDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var status = _statusService.GetByID(model.ID);
                await _statusService.DeleteAsSync(status);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new StatusCreateViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StatusCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var status = new Status
                {
                    ID = model.ID,
                    Name = model.Name,

                };
                await _statusService.CreateAsSync(status);
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
            var status = _statusService.GetByID(id);
            var model = new StatusEditViewModel();
            model.ID = status.ID;
            model.Name = status.Name;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StatusEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var status = new Status
                {
                    ID = model.ID,
                    Name = model.Name,
                };
                await _statusService.UpdateAsSync(status);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}