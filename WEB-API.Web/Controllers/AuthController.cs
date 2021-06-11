using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB_API.DAL.Models;
using WEB_API.DAL.ViewModels;

namespace WEB_API.Web.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    var applicationUser = new ApplicationUser()
                    {
                        UserName = model.Email,
                        Email = model.Email,
                    };

                    var result = await _userManager.CreateAsync(applicationUser, model.Password);
                    if (result.Succeeded)
                    {
                        return StatusCode(201);
                    }

                    ModelState.AddModelError("", "Registration failed");
                    return BadRequest(GetModelStateErrors(ModelState));
                }
                
                ModelState.AddModelError("", "User already exist");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }
        //TODO: Errors +-
        //TODO: Service to send confirmation email
        //TODO: Automapper
        //TODO: Roles
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =  await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return StatusCode(201);
                }
                
                return StatusCode(401);
            }
            
            return StatusCode(401);
        }
    }
}