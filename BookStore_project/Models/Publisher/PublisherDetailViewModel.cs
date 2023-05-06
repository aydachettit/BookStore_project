namespace BookStore_project.Models.Publisher
{
    public class PublisherDetailViewModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public List<Entity.Book>? lob { get; set; }
    }
}
