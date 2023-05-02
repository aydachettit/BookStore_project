using BookStore_project.Models.Customer;
using BookStore_project.Models.Shipment;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public ShipmentController(IShipmentService shipmentService, IWebHostEnvironment hostingEnvironment, IStatusService statusService, ICustomerService customerService, IBillService billService)
        {
            _shipmentService = shipmentService;
            _hostingEnvironment = hostingEnvironment;
            _statusService = statusService;
            _customerService = customerService;
            _billService = billService;
        }

        public IActionResult Index()
        {
            var model = _shipmentService.GetAll().Select(shipment => new ShipmentIndexViewModel
            {

                ID = shipment.ID,
                BillID = shipment.BillID,
                CustomerID = shipment.CustomerID,
                Shipment_Status_ID = shipment.Shipment_Status_ID,
            }).ToList();

            foreach (var item in model)
            {
                item.CustomerName = _customerService.GetByID(item.CustomerID).Name;
                //item.StatusName = _statusService.GetByID(item.Shipment_Status_ID).Name;
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
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
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var shipment = _shipmentService.GetByID(id);
            var BillID = _billService.GetByID(shipment.BillID).ID;
            var CustomerID = _customerService.GetByID(shipment.CustomerID).ID;
            var Ship_StatusID = _statusService.GetByID(shipment.Shipment_Status_ID).ID;

            var model = new ShipmentDetailViewModel
            {
                ID = shipment.ID,
                BillID = BillID,
                CustomerID = CustomerID,
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
        public IActionResult SearchResultPage(ShipmentSearchViewModel model)
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
                            shipment.CustomerName = _customerService.GetByID(shipment.CustomerID).Name;
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
                            shipment.CustomerName = _customerService.GetByID(shipment.CustomerID).Name;
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
                            shipment.CustomerName = _customerService.GetByID(shipment.CustomerID).Name;
                            shipment.StatusName = _statusService.GetByID(shipment.Shipment_Status_ID).Name;
                        }
                        return View(shipmentmodel);
                }
            }
            return View();
        }
    }
}
