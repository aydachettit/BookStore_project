namespace BookStore_project.Models.User
{
    public class UserChangePasswordViewModel
    {
        public string id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
