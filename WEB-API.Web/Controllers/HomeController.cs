using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB_API.DAL.Models;
using WEB_API.Web.Helpers;

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
        [Authorize(Roles = Roles.Admin)]
        public IActionResult GetInfo()
        {
            return Ok("Hello world!");
        }
        
        //DEBUG METHOD-------------------
        [HttpGet("init")]
        [AllowAnonymous]
        public  async Task InitializeDatabase()
        {
            const string adminEmail = "admin@admin.com";
            const string password = "Admin_145";
            
            if (await _roleManager.FindByNameAsync(Roles.Admin) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
            
            if (await _roleManager.FindByNameAsync(Roles.User) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.User));
            }
            
            if (await _userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new ApplicationUser() { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true};
                var result = await _userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, Roles.Admin);
                }
            }
        }
        //DEBUG METHOD-------------------------
    }
}