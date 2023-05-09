using BookStore_project.Models.BillDetail;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace BookStore_project.Controllers
{

    [Authorize(Roles = "Admin")]
    public class BillDetailController :Controller
    {
        private IBillDetailService _billDetailService;
        private ApplicationDbContext _context;
        private IBookService _bookService;
        private IBillService _billService;
        public BillDetailController(IBillDetailService billDetailService,
                                    ApplicationDbContext context
            ,
            IBookService bookService, IBillService billService
            )
        {
            _billDetailService = billDetailService;
            _context = context;
            _bookService = bookService;
            _billService = billService;
        }

        //public ActionResult Index()
        //{
        //    var model = _billDetailService.GetAll().Select(x => new BillDetailIndexViewModel
        //    {
        //        Bill_Detail_ID = x.Bill_Detail_ID,
        //        Amount = x.Amount,
        //        Price = x.Price,
        //    }).ToList();

        //    return View(model);
        //}

        [HttpGet]
        public async Task<IActionResult> Index (string SeachString)
        {
            ViewData["CurrentFilter"] = SeachString;
            var bill_detail = _billDetailService.GetAll().Select(bd => new BillDetailIndexViewModel
            {
                Bill_Detail_ID = bd.Bill_Detail_ID,
                Amount = bd.Amount,
                Price = bd.Price,
                Book_ID = bd.Book_ID,
                Bill_ID = bd.Bill_ID,
               

            }).ToList();

            return View(bill_detail);

        }

    }
}
