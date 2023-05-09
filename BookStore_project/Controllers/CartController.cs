using BookStore_project.Models.Cart;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;



namespace BookStore_project.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private IBookService _bookService;
        private IBillService _billService;
        private IBillDetailService _billDetailService;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(ICartService cartService, IBookService bookService, IBillService billService, IBillDetailService billDetailService, UserManager<IdentityUser> userManager)
        {
            _cartService = cartService;
            _bookService = bookService;
            _billService = billService;
            _billDetailService = billDetailService;
            _userManager = userManager;
        }


        //[CustomAuthorize]

        public IActionResult Index()
        {

            //if (!User.IsInRole("Admin"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}


            var cartItems = _cartService.GetCartItems().Select(c => new CartViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ImageURL = c.ImageURL,
                Price = c.Price,
                Quantity = c.Quantity,
                TotalPrice = c.TotalPrice
            }).ToList();

            
            return View(cartItems);
        }

        [HttpGet]
        
        public IActionResult AddToCart(int ID, int number)
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Customer"))
            {
                return RedirectToAction("Login", "User");
            }


            var book = _bookService.GetByID(ID);
            var item = new CartItem
            {
                Id = book.ID,
                Name = book.Name,
                ImageURL = book.Image_URL,
                Price = book.Price,
                Quantity = number,
            };
            item.TotalPrice = item.Quantity * Convert.ToInt32(item.Price);

            _cartService.AddToCart(item);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Checkout()
        {
            var cartItems = _cartService.GetCartItems().Select(c => new CartViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ImageURL = c.ImageURL,
                Price = c.Price,
                Quantity = c.Quantity,
                TotalPrice = c.TotalPrice
                
            }).ToList();

            var user = await _userManager.FindByNameAsync(User.Identity.Name) as IdentityUser;
            var bill = new Bill();
            DateTime currentDateTime = DateTime.Now;
            bill.Date = currentDateTime;
            bill.Employee_ID = 1;
            bill.Customer_ID = user.UserName;
            bill.Bill_status_ID = 1;
            
            var totalPrice = 0;
            foreach (var item in cartItems)
            {
                totalPrice = totalPrice + item.TotalPrice;
            }
            bill.Total_money = totalPrice;
            await _billService.CreateAsSync(bill);


            foreach(var item in cartItems)
            {
                var billDetail = new BillDetail();
                billDetail.Bill_ID = bill.ID;
                billDetail.Book_ID = item.Id;
                billDetail.Amount = item.Quantity;
                billDetail.Price = item.Price;
                await _billDetailService.CreateAsAsync(billDetail);
            }
            foreach (var item in cartItems)
            {
                _cartService.RemoveFromCart(item.Id);
            }

            return RedirectToAction("Index");
        }


        public async  Task<IActionResult> RemoveFromCart(int bookId)
        {
           

                await _cartService.RemoveFromCart(bookId);
                return RedirectToAction("Index", "Cart");
            


        }
    }
}
