using Org.BouncyCastle.Bcpg.OpenPgp;

namespace BookStore_project.Models.User
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public List<Entity.Bill> ListOfBill { get; set; }
    }
}
