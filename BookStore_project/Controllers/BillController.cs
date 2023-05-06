using BookStore_project.Models.Bill;
using Entity;
using Service;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStore_project.Controllers {
    public class BillController : Controller {
        private IBillService _billService;
        private IStatusService _StatusService;

        public BillController(IBillService billService, IStatusService StatusService)
        {
            _StatusService = StatusService;
            _billService = billService;
        }
        [CustomAuthorize]
         public IActionResult Index(int ? page){
            var model = _billService.GetAll().Select( Bill => new BillIndexViewModel
            {
                ID = Bill.ID,
                Date = Bill.Date,
                Total_money = Bill.Total_money,
                Customer_ID = Bill.Customer_ID,
                Employee_ID = Bill.Employee_ID,
                Bill_status_ID = _StatusService.GetByID(Bill.Bill_status_ID).Name
            }).OrderBy(x=>x.ID).ToList();
            int pagesize = 1;
            int pagenumber = (page ?? 1);

            return View(model.ToPagedList(pagenumber, pagesize));

        }
        [HttpGet]
        [CustomAuthorize]
        public IActionResult Detail(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var bill = _billService.GetByID(id);
            var model = new BillDetailViewModel();
            model.ID = bill.ID;
            model.Date = bill.Date;
            model.Total_money = bill.Total_money;
            model.Customer_ID = bill.Customer_ID;
            model.Employee_ID = bill.Employee_ID;
            model.Bill_status_ID = bill.Bill_status_ID;
            var listdetail = _billService.GetBillDetailByBill(id);
            model.ListOfBillDetail = listdetail;
            return View(model);
        }
        [HttpGet]
        [CustomAuthorize]
        public IActionResult Delete(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var bill = _billService.GetByID(id);
            var model = new BillDeleteViewModel();
            model.ID = bill.ID;
            model.Date = bill.Date;
            model.Total_money = bill.Total_money;
            model.Customer_ID = bill.Customer_ID;
            model.Employee_ID = bill.Employee_ID;
            model.Bill_status_ID = bill.Bill_status_ID;
            return View(model);
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public async Task<IActionResult> Delete(BillDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bill = _billService.GetByID(model.ID);
                await _billService.DeleteAsSync(bill);
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        [CustomAuthorize]
        public IActionResult Create()
        {
            var model = new BillCreateViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public async Task<IActionResult> Create(BillCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bill = new Bill
                {
                    ID = model.ID,
                    Date = model.Date,
                    Total_money = model.Total_money,
                    Customer_ID = model.Customer_ID,
                    Employee_ID = model.Employee_ID,
                    Bill_status_ID = model.Bill_status_ID

                };
                await _billService.CreateAsSync(bill);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        [CustomAuthorize]
        public IActionResult Edit(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var bill = _billService.GetByID(id);
            var model = new BillDeleteViewModel();
            model.ID = bill.ID;
            model.Date = bill.Date;
            model.Total_money = bill.Total_money;
            model.Customer_ID = bill.Customer_ID;
            model.Employee_ID = bill.Employee_ID;
            model.Bill_status_ID = bill.Bill_status_ID;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public async Task<IActionResult> Edit(BillEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bill = new Bill
                {
                    ID = model.ID,
                    Date = model.Date,
                    Total_money = model.Total_money,
                    Customer_ID = model.Customer_ID,
                    Employee_ID = model.Employee_ID,
                    Bill_status_ID = model.Bill_status_ID
                };
                await _billService.UpdateAsSync(bill);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        [CustomAuthorize]
        public IActionResult Process(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var bill = _billService.GetByID(id);
            var model = new BillProcessViewModel();
            var user = _billService.FindBillByUser(bill.Customer_ID);
            model.Date = bill.Date;
            model.Total = bill.Total_money;
            var statusName = _StatusService.GetByID(bill.Bill_status_ID).Name.ToString();
            model.status = statusName;
            if (statusName.Equals("Canceled") || bill.Bill_status_ID==3)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public async Task<IActionResult> Process(BillProcessViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bill = _billService.GetByID(model.id);
                bill.Bill_status_ID++;
                await _billService.UpdateAsSync(bill);
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        [CustomAuthorize]
        public IActionResult Canceled(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }
            var bill = _billService.GetByID(id);
            var model = new BillCanceledViewModel();
            var user = _billService.FindBillByUser(bill.Customer_ID);
            model.Date = bill.Date;
            model.Total = bill.Total_money;
            var statusName = _StatusService.GetByID(bill.Bill_status_ID).Name.ToString();
            model.status = statusName;
            if (statusName.Equals("Canceled"))
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public async Task<IActionResult> Canceled(BillCanceledViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bill = _billService.GetByID(model.id);
                bill.Bill_status_ID = 4;
                await _billService.UpdateAsSync(bill);
                return RedirectToAction("Index");
            }

            return View();
        }



    }
}
