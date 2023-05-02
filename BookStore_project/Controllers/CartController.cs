using BookStore_project.Models.Cart;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace BookStore_project.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private IBookService _bookService;

        public CartController(ICartService cartService, IBookService bookService)
        {
            _cartService = cartService;
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            return View(cartItems);
        }

        [HttpGet]
        public IActionResult AddToCart(int bookId)
        {
            var book = _bookService.GetByID(bookId);
            var item = new CartItem
            {
                Id = book.ID,
                Name = book.Name,
                ImageURL = book.Image_URL,
                Price = book.Price,
                Quantity = 1,
            };
            item.TotalPrice = item.Quantity * Convert.ToInt32(item.Price);

            _cartService.AddToCart(item);

            return RedirectToAction("/Home/Index");
        }

        public IActionResult RemoveFromCart(int bookId)
        {
            _cartService.RemoveFromCart(bookId);

            return RedirectToAction("Index");
        }
    }
}
