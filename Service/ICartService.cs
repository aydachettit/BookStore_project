using Entity;


namespace Service
{
    public interface ICartService
    {
        void AddToCart(CartItem item);
        void RemoveFromCart(int productId);
        List<CartItem> GetCartItems();
    }
}
