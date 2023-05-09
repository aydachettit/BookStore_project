using BookStore_project.Models.Employee;
using DataAccess;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Service;
using System.Data;

namespace BookStore_project.Controllers
{

    [Authorize(Roles = "Admin")]

    public class EmployeeController : Controller
    {

        private IEmployeeService _employeeService;
        private ApplicationDbContext _context;
        private IWebHostEnvironment _hostingEnvironment;



        public EmployeeController(IEmployeeService employeeService, ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _employeeService = employeeService;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    var model = _employeeService.GetAll().Select(employee => new EmployeeIndexViewModel
        //    {

        //        employeeID = employee.employeeID,
        //        employeeName = employee.employeeName,
        //        employeeDate_Join= employee.employeeDate_Join,
        //        employeeGender= employee.employeeGender,
        //        employeePhone_Number= employee.employeePhone_Number,



        //    }).ToList() ;


        //    return View(model);
        //}
        public async Task<IActionResult> Index(string SearchString, int? page)
        {
            ViewData["CurrentFilter"] = SearchString;
            //var employee = from e in _context.Employee
            //               select e;

            var model = _employeeService.GetAll().Select(employee => new EmployeeIndexViewModel
            {

                employeeID = employee.employeeID,
                employeeName = employee.employeeName,
                employeeDate_Join = employee.employeeDate_Join,
                employeeGender = employee.employeeGender,
                employeePhone_Number = employee.employeePhone_Number,
                employeeImage = employee.employeeImage,


            }).AsEnumerable();

            int page_size = 5;
            int page_number = (page ?? 1);

            if (!String.IsNullOrEmpty(SearchString))
            {
                // dòng này dùng để tìm tên nhập trên input có tồn tại trong database không ?
                model = model.Where(e => e.employeeName.ToUpper().Contains(SearchString.ToUpper()));
            }

            return View(model.ToPagedList(page_number, page_size));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    employeeID = model.employeeID,
                    employeeName = model.employeeName,
                    employeeGender = model.employeeGender,
                    employeeEmail = model.employeeEmail,
                    employeePhone_Number = model.employeePhone_Number,
                    employeeDOB = model.employeeDOB,
                    employeeAddress = model.employeeAddress,
                    employeeDate_Join = model.employeeDate_Join,
                };

                if (model.employeeImage != null && model.employeeImage.Length > 0)
                {
                    var uploadDir = @"img/employees";
                    var fileName = Path.GetFileNameWithoutExtension(model.employeeImage.FileName);
                    var extension = Path.GetExtension(model.employeeImage.FileName);
                    var webrootPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webrootPath, uploadDir, fileName);
                    await model.employeeImage.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.employeeImage = "/" + uploadDir + "/" + fileName;
                }
                await _employeeService.CreateAsAsync(employee);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var employee = _employeeService.GetByID(id);
            var model = new EmployeeDetailViewModel
            {
                employeeID = employee.employeeID,
                employeeName = employee.employeeName,
                employeeDate_Join = employee.employeeDate_Join,
                employeeGender = employee.employeeGender,
                employeePhone_Number = employee.employeePhone_Number,
                employeeImage = employee.employeeImage,
                employeeAddress = employee.employeeAddress,
                employeeDOB = employee.employeeDOB,
                employeeEmail = employee.employeeEmail,

            };

            return View(model);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {

            if (id.ToString() == null)
            {
                return NotFound();
            }
            var employeeToDelete = _employeeService.GetByID(id);
            EmployeeDeleteViewModel modelDelete = new EmployeeDeleteViewModel();
            modelDelete.employeeID = employeeToDelete.employeeID;
            modelDelete.employeeName = employeeToDelete.employeeName;

            return View(modelDelete);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteByID(int id)
        {
            if (id.ToString() == null)
                return NotFound();
            _employeeService.DeleteByID(id);
            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {

                var employee = _employeeService.GetByID(model.employeeID);

                await _employeeService.DeleteAsAsync(employee);
                return RedirectToAction("Index");

            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id.ToString() == null)
                return NotFound();

            var employee = _employeeService.GetByID(id);
            EmployeeEditViewModel model = new EmployeeEditViewModel();


            model.employeeID = employee.employeeID;
            model.employeeName = employee.employeeName;
            model.employeeGender = employee.employeeGender;
            model.employeeDate_Join = employee.employeeDate_Join;
            model.employeePhone_Number = employee.employeePhone_Number;


            return View(model);
        }

        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetByID(model.employeeID);

                //employee.employeeID = model.employeeID;
                employee.employeeGender = model.employeeGender;
                employee.employeeName = model.employeeName;
                employee.employeeDate_Join = model.employeeDate_Join;
                employee.employeePhone_Number = model.employeePhone_Number;
                employee.employeeEmail = model.employeeEmail;
                employee.employeeDOB = model.employeeDOB;
                employee.employeeAddress = model.employeeAddress;



                if (model.employeeImage != null && model.employeeImage.Length > 0)
                {
                    var uploadDir = @"img/employees";
                    var fileName = Path.GetFileNameWithoutExtension(model.employeeImage.FileName);
                    var extension = Path.GetExtension(model.employeeImage.FileName);
                    var webrootPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webrootPath, uploadDir, fileName);
                    await model.employeeImage.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.employeeImage = "/" + uploadDir + "/" + fileName;
                }

                await _employeeService.UpdateAsAsync(employee);
                return RedirectToAction("Index");
            }

            return View(model);


        }


    }
}
