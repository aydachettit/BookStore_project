using BookStore_project.Models.BillDetail;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace BookStore_project.Controllers
{
    public class BillDetailController :Controller
    {
        private IBillDetailService _billDetailService;
        private IBookService _bookService;
        private IBillService _billService;
        public BillDetailController(IBillDetailService billDetailService, IBookService bookService, IBillService billService)
        {
            _billDetailService = billDetailService;
            _bookService = bookService;
            _billService = billService;
        }

        public ActionResult Index()
        {
            var model = _billDetailService.GetAll().Select(x => new BillDetailIndexViewModel
            {
                Bill_Detail_ID = x.Bill_Detail_ID,
                Amount = x.Amount,
                Price = x.Price,
            }).ToList();

            return View(model);
        }


    }
}
