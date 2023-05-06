using BookStore_project.Models.Bill;
using BookStore_project.Models.Customer;
using BookStore_project.Models.Shipment;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;
using Service;
using Service.implementation;
using Service.Implementation;

namespace BookStore_project.Controllers
{
    public class ShipmentController : Controller
    {
        public readonly IShipmentService _shipmentService;
        public readonly IWebHostEnvironment _hostingEnvironment;
        public readonly IStatusService _statusService;
        public readonly ICustomerService _customerService;
        public readonly IBillService _billService;

        private readonly UserManager<IdentityUser> _userManager;



        public ShipmentController(UserManager<IdentityUser> userManager, IShipmentService shipmentService, IWebHostEnvironment hostingEnvironment, IStatusService statusService, ICustomerService customerService, IBillService billService)
        {
            _userManager = userManager;
            _shipmentService = shipmentService;
            _hostingEnvironment = hostingEnvironment;
            _statusService = statusService;
            _customerService = customerService;
            _billService = billService;
        }
        [HttpGet]
       
        public IActionResult Process(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var ship = _shipmentService.GetByID(id);
            var model = new ShipmenrtProcessViewModel();
            model.id = ship.ID;
            model.Status = _statusService.GetByID(ship.Shipment_Status_ID).Name;


            if (model.Status.Equals("Canceled") || model.Status.Equals("Shipped"))
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Process(ShipmenrtProcessViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var ship = _shipmentService.GetByID(model.id);
                ship.Shipment_Status_ID++;
                await _shipmentService.UpdateAsAsync(ship);
                var bill = _billService.GetByID(ship.BillID);
                bill.Bill_status_ID = ship.Shipment_Status_ID;
                await _billService.UpdateAsSync(bill);
                return RedirectToAction("Index");
            }

            return View();
        }
         [Authorize(Roles = "Admin")]
        public IActionResult Index(int? page)
        {
            
            var model = _shipmentService.GetAll().Select(shipment => new ShipmentIndexViewModel
            {

                ID = shipment.ID,
                BillID = shipment.BillID,
                CustomerID = shipment.CustomerID,
                CustomerName = shipment.CustomerName,
                Shipment_Status_ID = shipment.Shipment_Status_ID,
                StatusName = _statusService.GetByID(shipment.Shipment_Status_ID).Name
            }).Where(x => x.Shipment_Status_ID >= 2).OrderBy(x=>x.ID).ToList();

           
            int pagesize = 1;
            int pagenumber = (page ?? 1);
            return View(model.ToPagedList(pagenumber, pagesize));
        }

        [HttpGet]
        
        public IActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ShipmentCreateViewModel();
            IEnumerable<SelectListItem> BillList = _billService.GetAll().Select(bill => new SelectListItem
            {
                Value = bill.ID.ToString(),
            }).ToList();

            IEnumerable<SelectListItem> CustomerList = _customerService.GetAll().Select(customer => new SelectListItem
            {
                Value = customer.ID.ToString(),
                Text = customer.Name,
            }).ToList();

            IEnumerable<SelectListItem> StatusList = _statusService.GetAll().Select(status => new SelectListItem
            {
                Value = status.ID.ToString(),
                Text = status.Name,
            }).ToList();

            model.Bills = BillList;
            model.Customers = CustomerList;
            model.Status = StatusList;
            return View(model);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShipmentCreateViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var Shipment = new Shipment
                {
                    ID = model.ID,
                    BillID = model.BillID,
                    CustomerID = model.CustomerID,
                    Shipment_Status_ID = model.Shipment_Status_ID,
                };
                await _shipmentService.CreateAsAsync(Shipment);
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
            var shipment = _shipmentService.GetByID(id);
            var model = new ShipmentEditViewModel
            {
                ID = shipment.ID,
                BillID = shipment.BillID,
                CustomerID = shipment.CustomerID,
                Shipment_Status_ID = shipment.Shipment_Status_ID
            };

            IEnumerable<SelectListItem> BillList = _billService.GetAll().Select(bill => new SelectListItem
            {
                Value = bill.ID.ToString(),
            }).ToList();

            IEnumerable<SelectListItem> CustomerList = _customerService.GetAll().Select(customer => new SelectListItem
            {
                Value = customer.ID.ToString(),
                Text = customer.Name,
            }).ToList();

            IEnumerable<SelectListItem> StatusList = _statusService.GetAll().Select(status => new SelectListItem
            {
                Value = status.ID.ToString(),
                Text = status.Name,
            }).ToList();
            model.Bills = BillList;
            model.Customers = CustomerList;
            model.Status = StatusList;

            return View(model);
        }
        
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ShipmentEditViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var Shipment = new Shipment
                {
                    ID = model.ID,
                    BillID = model.BillID,
                    CustomerID = model.CustomerID,
                    Shipment_Status_ID = model.Shipment_Status_ID
                };
                await _shipmentService.UpdateAsAsync(Shipment);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
       
        public IActionResult Delete(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var shipment = _shipmentService.GetByID(id);
            if (shipment == null)
            {
                return NotFound();
            }
            var model = new ShipmentDeleteViewModel
            {
                ID = shipment.ID,
                CustomerName = shipment.CustomerName,
                Shipment_Status_ID = shipment.Shipment_Status_ID
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Delete(ShipmentDeleteViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var shipment = _shipmentService.GetByID(model.ID);
            if (shipment == null)
            {
                return NotFound();
            }
            await _shipmentService.DeleteAsAsync(shipment);
            return RedirectToAction("Index");
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

            var shipment = _shipmentService.GetByID(id);
            var BillID = _billService.GetByID(shipment.BillID).ID;
            var Ship_StatusID = _statusService.GetByID(shipment.Shipment_Status_ID).ID;

            var model = new ShipmentDetailViewModel
            {
                ID = shipment.ID,
                BillID = BillID,
                CustomerID = shipment.CustomerID,
                Shipment_Status_ID = Ship_StatusID
            };
            return View(model);
        }

        [HttpGet]
        
        public IActionResult SearchPage()
        {
            var model = new ShipmentSearchViewModel();
            return View(model);
        }
        [HttpPost]
        
        public async Task<IActionResult> SearchResultPageAsync(ShipmentSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                int KeyID = model.SearchKeyID;
                string keyWord = model.Keyword;

                switch (KeyID)
                {
                    case 1:
                        var shipmentmodel = _shipmentService.GetShipmentByBillID(Int32.Parse(keyWord)).Select(shipment => new ShipmentIndexViewModel
                        {
                            ID = shipment.ID,
                            BillID = shipment.BillID,
                            CustomerID = shipment.CustomerID,
                            Shipment_Status_ID = shipment.Shipment_Status_ID
                        }).ToList();
                        foreach (var shipment in shipmentmodel)
                        {
                            var temp = await _userManager.FindByIdAsync(shipment.CustomerID) as IdentityUser;
                            shipment.CustomerName = temp.UserName;
                            shipment.StatusName = _statusService.GetByID(shipment.Shipment_Status_ID).Name;
                        }
                        return View(shipmentmodel);
                    case 2:
                        shipmentmodel = _shipmentService.GetShipmentByCustomerName(keyWord).Select(shipment => new ShipmentIndexViewModel
                        {
                            ID = shipment.ID,
                            BillID = shipment.BillID,
                            CustomerID = shipment.CustomerID,
                            Shipment_Status_ID = shipment.Shipment_Status_ID
                        }).ToList();
                        foreach (var shipment in shipmentmodel)
                        {
                            var temp = await _userManager.FindByIdAsync(shipment.CustomerID) as IdentityUser;
                            shipment.CustomerName = temp.UserName;
                            shipment.StatusName = _statusService.GetByID(shipment.Shipment_Status_ID).Name;
                        }
                        return View(shipmentmodel);
                    case 3:
                        shipmentmodel = _shipmentService.GetShipmentByStatusID(Int32.Parse(keyWord)).Select(shipment => new ShipmentIndexViewModel
                        {
                            ID = shipment.ID,
                            BillID = shipment.BillID,
                            CustomerID = shipment.CustomerID,
                            Shipment_Status_ID = shipment.Shipment_Status_ID
                        }).ToList();
                        foreach (var shipment in shipmentmodel)
                        {
                            var temp = await _userManager.FindByIdAsync(shipment.CustomerID) as IdentityUser;
                            shipment.CustomerName = temp.UserName;
                            shipment.StatusName = _statusService.GetByID(shipment.Shipment_Status_ID).Name;
                        }
                        return View(shipmentmodel);
                }
            }
            return View();
        }
    }
}
