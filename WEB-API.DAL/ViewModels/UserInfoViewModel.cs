using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace WEB_API.DAL.ViewModels
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