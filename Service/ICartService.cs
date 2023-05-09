using Entity;


namespace Service
{
    public interface ICartService
    {
        void AddToCart(CartItem item);
        Task RemoveFromCart(int productId);
        List<CartItem> GetCartItems();
    }
}
