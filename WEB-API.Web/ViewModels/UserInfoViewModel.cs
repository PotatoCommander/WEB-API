using System.ComponentModel.DataAnnotations;

namespace WEB_API.Web.ViewModels
{
    public class UserInfoViewModel
    {
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }
        
        public string Address { get; set; }
    }
}