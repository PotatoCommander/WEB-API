using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Business.Interfaces;
using WEB_API.DAL.Models;
using WEB_API.DAL.ViewModels;

namespace WEB_API.Web.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : BaseController
    {
        private UserManager<ApplicationUser> _userManager;
        private IEmailService _emailService;

        public UserController(UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpPut("UpdateUser")]
        [Authorize]
        public async Task<ActionResult> UpdateUser(UserInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    if (model.Email != null)
                    {
                        if (user.Email == model.Email)
                        {
                            ModelState.AddModelError("", "New email is similar to current");
                            return BadRequest(GetModelStateErrors(ModelState));
                        }

                        user.NewEmail = model.Email;
                        var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.Email);
                        var confirmationUrl = Url.Action("ChangeEmail", "User",
                            new {userId = user.Id, changeToken = token}, HttpContext.Request.Scheme);
                        await _emailService.Send(model.Email, "New email confirmation.", confirmationUrl);
                    }

                    user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;
                    user.Address = model.Address ?? user.Address;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }

                    ModelState.AddModelError("", "Failed to update user");
                    return BadRequest(GetModelStateErrors(ModelState));
                }

                ModelState.AddModelError("", "Failed to find user");
                return Unauthorized(GetModelStateErrors(ModelState));
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }

        [HttpGet("ChangeEmail")]
        public async Task<ActionResult> ChangeEmail(string userId, string changeToken)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(changeToken))
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
            
            if (user.NewEmail == null)
            {
                ModelState.AddModelError("","New email doesnt exist");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            var changeResult = await _userManager.ChangeEmailAsync(user, user.NewEmail, changeToken);
            if (changeResult.Succeeded)
            {
                user.NewEmail = null;
                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    return Ok("Changed");
                }
            }

            ModelState.AddModelError("", "Changing failed");
            return BadRequest(GetModelStateErrors(ModelState));
        }
    }
}