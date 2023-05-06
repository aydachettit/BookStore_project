namespace BookStore_project.Models.Book
{
    public class CreateProductDetailViewModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public DateTime PublicDate { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }

        public string? Description { get; set; }
        public string? Image_URL { get; set; }

        public int AuthorID { get; set; }

        public string? Authors;

        public int PublisherID { get; set; }

        public string? Publishers;
        public int CategoryID { get; set; }

        public string? Categories;
    }
}
