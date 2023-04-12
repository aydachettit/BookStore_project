namespace BookStore_project.Models.Customer
{
    public class CustomerCreateViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
    }
}
