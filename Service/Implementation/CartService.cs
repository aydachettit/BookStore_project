using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System.Text;

namespace Service.Implementation
{
    public class CartService : ICartService
    {
        private const string SessionKey = "Cart";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddToCart(CartItem item)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(i => i.Id == item.Id);

            if (existingItem == null)
            {
                cart.Add(item);
            }
            else
            {
                existingItem.Quantity += item.Quantity;
            }

            SaveCart(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var itemToRemove = cart.FirstOrDefault(i => i.Id == productId);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                SaveCart(cart);
            }
        }

        public List<CartItem> GetCartItems()
        {
            return GetCart();
        }

        private List<CartItem> GetCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var data = session.Get(SessionKey);

            if (data == null)
            {
                return new List<CartItem>();
            }

            return JsonConvert.DeserializeObject<List<CartItem>>(Encoding.UTF8.GetString(data));
        }

        private void SaveCart(List<CartItem> cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var json = JsonConvert.SerializeObject(cart);
            session.Set(SessionKey, Encoding.UTF8.GetBytes(json));
        }
    }

}
