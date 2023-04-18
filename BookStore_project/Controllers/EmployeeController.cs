using BookStore_project.Models.Employee;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace BookStore_project.Controllers
{
    public class EmployeeController : Controller
    {

        private IEmployeeService _employeeService;


        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

        }

        public IActionResult Index()
        {
            var model = _employeeService.GetAll().Select(employee => new EmployeeIndexViewModel
            {

                employeeID = employee.employeeID,
                employeeName = employee.employeeName,
                employeeDate_Join= employee.employeeDate_Join,
                employeeGender= employee.employeeGender,
                employeePhone_Number= employee.employeePhone_Number,
               
                

            }).ToList() ;


            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create (EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    employeeID = model.employeeID,
                    employeeName = model.employeeName,
                    employeeDate_Join = model.employeeDate_Join,
                    employeeGender = model.employeeGender,
                    employeePhone_Number= model.employeePhone_Number





                };

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
                employeePhone_Number = employee.employeePhone_Number

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
        public IActionResult DeleteByID (int id)
        {
            if (id.ToString() == null)
                return NotFound();
            _employeeService.DeleteByID(id);
            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete (EmployeeDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    employeeID = model.employeeID,
                    employeeName = model.employeeName
                };
                await _employeeService.DeleteAsAsync(employee);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit (int id)
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

        public async Task<IActionResult> Edit ( EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetByID (model.employeeID);

                employee.employeeID = model.employeeID;
                employee.employeeGender = model.employeeGender;
                employee.employeeName = model.employeeName;
                employee.employeeDate_Join = model.employeeDate_Join;
                employee.employeePhone_Number= model.employeePhone_Number;


                await _employeeService.CreateAsAsync(employee);
                return RedirectToAction("Index");
            }

            return View(model);
            

        }


    }
}
