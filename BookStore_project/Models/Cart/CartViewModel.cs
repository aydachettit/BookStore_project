namespace BookStore_project.Models.Cart
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int TotalPrice { get; set; }
    }
}
