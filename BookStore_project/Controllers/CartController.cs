using BookStore_project.Models.Cart;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace BookStore_project.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            return View(cartItems);
        }

        public IActionResult AddToCart(CartViewModel model)
        {
            var item = new CartItem
            {
                Id = model.Id,
                Name = model.Name,
                ImageURL = model.ImageURL,
                Price = model.Price,
                Quantity = model.Quantity
            };

            _cartService.AddToCart(item);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);

            return RedirectToAction("Index");
        }
    }
}
