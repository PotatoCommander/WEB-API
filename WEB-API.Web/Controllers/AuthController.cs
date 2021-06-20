using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Business.Interfaces;
using WEB_API.DAL.Models;
using WEB_API.Web.ViewModels;

namespace WEB_API.Web.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IEmailService _emailService;
        private IMapper _mapper;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IEmailService emailService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _mapper = mapper;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    var applicationUser = _mapper.Map<ApplicationUser>(model);
                    var result = await _userManager.CreateAsync(applicationUser, model.Password);
                    if (result.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                        var confirmationUrl = Url.Action("ConfirmEmail", "Auth",
                            new {userId = applicationUser.Id, confirmToken = token}, HttpContext.Request.Scheme);
                        await _emailService.Send(applicationUser.Email, "New account confirmation.", confirmationUrl);
                        return StatusCode(201);
                    }

                    ModelState.AddModelError("", "Registration failed");
                    return BadRequest(GetModelStateErrors(ModelState));
                }

                ModelState.AddModelError("", "User with such email already exist");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return StatusCode(201);
                }

                return Unauthorized(GetModelStateErrors(ModelState));
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(string userId, string confirmToken)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(confirmToken))
            {
                ModelState.AddModelError("", "Invalid link");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return NotFound(GetModelStateErrors(ModelState));
            }

            if (user.EmailConfirmed)
            {
                ModelState.AddModelError("", "User already confirmed");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            var result = await _userManager.ConfirmEmailAsync(user, confirmToken);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                return Ok("Confirmed");
            }

            ModelState.AddModelError("", "Confirmation failed");
            return BadRequest(GetModelStateErrors(ModelState));
        }
    }
}