using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace WEB_API.DAL.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string NewEmail { get; set; }
        public string Address { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}