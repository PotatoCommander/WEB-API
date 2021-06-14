using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB_API.DAL.Models;

namespace WEB_API.Web.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public HomeController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        [HttpGet("GetInfo")]
        [Authorize(Roles = "admin")]
        public IActionResult GetInfo()
        {
            return Ok("Hello world!");
        }
        
        //DEBUG METHOD-------------------
        [HttpGet("init")]
        public  async Task InitializeDatabase()
        {
            const string adminEmail = "admin@admin.com";
            const string password = "Admin_145";
            
            if (await _roleManager.FindByNameAsync("admin") == null)
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
            }
            
            if (await _roleManager.FindByNameAsync("user") == null)
            {
                await _roleManager.CreateAsync(new IdentityRole("user"));
            }
            
            if (await _userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new ApplicationUser() { Email = adminEmail, UserName = adminEmail };
                var result = await _userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
        //DEBUG METHOD-------------------------
    }
}