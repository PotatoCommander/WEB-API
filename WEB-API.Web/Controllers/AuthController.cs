using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private ILogger<OrderController> _logger;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IEmailService emailService, IMapper mapper, ILogger<OrderController> logger)
        {
            _logger = logger;
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
                        _logger
                            .LogInformation($"Email: {applicationUser.Email} Id: {applicationUser.Id} user registered.");
                        return StatusCode(201);
                    }
                    
                    _logger.LogInformation($"Unsuccessful attempt to sign up by address: " +
                                           $"{HttpContext.Connection.RemoteIpAddress} email: {model.Email} ");
                    ModelState.AddModelError("", "Registration failed");
                    return BadRequest(GetModelStateErrors(ModelState));
                }
                
                _logger.LogInformation($"Attempt to sign up with already existing email: {model.Email}");
                ModelState.AddModelError("", "User with such email already exist");
                return BadRequest(GetModelStateErrors(ModelState));
            }
            
            _logger.LogInformation($"Attempt to sign up with invalid model");
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
                    _logger.LogInformation($"Email: {model.Email}  user signed in.");
                    return StatusCode(201);
                }
                
                _logger.LogInformation($"Attempt to user login ({model.Email}. Invalid credentials.)");
                return Unauthorized(GetModelStateErrors(ModelState));
            }
            
            _logger.LogInformation($"Attempt to sign in with invalid model");
            return BadRequest(GetModelStateErrors(ModelState));
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(string userId, string confirmToken)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(confirmToken))
            {
                _logger.LogWarning($"Confirm email method calling with invalid parameters. (userId: {userId})");
                ModelState.AddModelError("", "Invalid link");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"Confirm email method calling to not existing user (userId: {userId})");
                ModelState.AddModelError("", "User not found");
                return NotFound(GetModelStateErrors(ModelState));
            }

            if (user.EmailConfirmed)
            {
                _logger.LogInformation($"Confirm email method calling to not existing user (userId: {userId})");
                ModelState.AddModelError("", "User already confirmed");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            var result = await _userManager.ConfirmEmailAsync(user, confirmToken);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Email confirmed for user. (userId: {userId}, email: {user.Email})");
                await _userManager.AddToRoleAsync(user, "user");
                return Ok("Confirmed");
            }
            
            _logger.LogError($"Error occured while confirming email. ({user.Email})");
            ModelState.AddModelError("", "Confirmation failed");
            return BadRequest(GetModelStateErrors(ModelState));
        }
    }
}