using Microsoft.AspNetCore.Identity;

namespace WEB_API.DAL.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Address { get; set; }
    }
}