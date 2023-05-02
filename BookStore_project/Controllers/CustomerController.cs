using BookStore_project.Models.Customer;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace BookStore_project.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CustomerController(ICustomerService customerService, IWebHostEnvironment hostingEnvironment)
        {
            _customerService = customerService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var model = _customerService.GetAll().Select(customer => new CustomerIndexViewModel
            {

                ID = customer.ID,
                Name = customer.Name,
                Gender = customer.Gender,
                Phone = customer.Phone,
                Address = customer.Address,
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CustomerCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    ID = model.ID,
                    Name = model.Name,
                    Gender = model.Gender,
                    Phone = model.Phone,
                    Address = model.Address

                };

                await _customerService.CreateAsAsync(customer);
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
            var customer = _customerService.GetByID(id);
            var model = new CustomerDetailViewModel
            {
                ID = customer.ID,
                Name = customer.Name,
                Gender = customer.Gender,
                Phone = customer.Phone,
                Address = customer.Address
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = _customerService.GetByID(id);
            if (customer == null)
            {
                return NotFound();
            }
            var model = new CustomerDeleteViewModel
            {
                ID = customer.ID,
                Name = customer.Name,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CustomerDeleteViewModel model)
        {
            var book = _customerService.GetByID(model.ID);
            if (book == null)
            {
                return NotFound();
            }
            await _customerService.DeleteByID(model.ID);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var customer = _customerService.GetByID(id);
            var model = new CustomerEditViewModel
            {
                ID = customer.ID,
                Name = customer.Name,
                Gender = customer.Gender,
                Phone = customer.Phone,
                Address = customer.Address
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    ID = model.ID,
                    Name = model.Name,
                    Gender = model.Gender,
                    Phone = model.Phone,
                    Address = model.Address
                };
                await _customerService.UpdateAsAsync(customer);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SearchPage()
        {
            var model = new CustomerSearchViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult SearchResultPage(CustomerSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                int KeyID = model.SearchKeyID;
                string keyWord = model.Keyword;

                switch (KeyID)
                {
                    case 1:
                        var customerModel = _customerService.GetCustomerByName(keyWord).Select(customer => new CustomerIndexViewModel
                        {
                            ID = customer.ID,
                            Name = customer.Name,
                            Gender = customer.Gender,
                            Phone = customer.Phone,
                            Address = customer.Address
                        }).ToList();
                        return View(customerModel);
                    case 2:
                        customerModel = _customerService.GetCustomerByGender(keyWord).Select(customer => new CustomerIndexViewModel
                        {
                            ID = customer.ID,
                            Name = customer.Name,
                            Gender = customer.Gender,
                            Phone = customer.Phone,
                            Address = customer.Address
                        }).ToList();
                        return View(customerModel);
                    case 3:
                        customerModel = _customerService.GetCustomerByPhone(keyWord).Select(customer => new CustomerIndexViewModel
                        {
                            ID = customer.ID,
                            Name = customer.Name,
                            Gender = customer.Gender,
                            Phone = customer.Phone,
                            Address = customer.Address
                        }).ToList();
                        return View(customerModel);
                    case 4:
                        customerModel = _customerService.GetCustomerByAddress(keyWord).Select(customer => new CustomerIndexViewModel
                        {
                            ID = customer.ID,
                            Name = customer.Name,
                            Gender = customer.Gender,
                            Phone = customer.Phone,
                            Address = customer.Address
                        }).ToList();
                        return View(customerModel);
                }
            }
            return View();
        }
    }
}
