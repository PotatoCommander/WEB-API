using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Business.Interfaces;
using WEB_API.DAL.Models;
using WEB_API.Web.ViewModels;
using WEB_API.Web.ViewModels.User;

namespace WEB_API.Web.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : BaseController
    {
        private UserManager<ApplicationUser> _userManager;
        private IEmailService _emailService;
        private IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, IEmailService emailService, IMapper mapper)
        {
            _userManager = userManager;
            _emailService = emailService;
            _mapper = mapper;
        }

        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<ActionResult> ChangePassword(PasswordChangingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.OldPassword == model.NewPassword)
                {
                    ModelState.AddModelError("", "New password is same to old");
                    return BadRequest(GetModelStateErrors(ModelState));
                }

                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }

                    ModelState.AddModelError("", "Password changing failed");
                    return BadRequest(GetModelStateErrors(ModelState));
                }

                ModelState.AddModelError("", "User not found");
                return NotFound(GetModelStateErrors(ModelState));
            }

            return BadRequest(GetModelStateErrors(ModelState));
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
                            ModelState.AddModelError("", "New email is same to current");
                            return BadRequest(GetModelStateErrors(ModelState));
                        }

                        user.NewEmail = model.Email;
                        var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.Email);
                        var confirmationUrl = Url.Action("ChangeEmailConfirm", "User",
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
        public async Task<ActionResult> ChangeEmailConfirm(string userId, string changeToken)
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

        [HttpGet("userinfo")]
        [Authorize]
        public async Task<ActionResult> GetUserInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return NotFound(GetModelStateErrors(ModelState));
            }

            return new JsonResult(_mapper.Map<UserInfoViewModel>(user));
        }
    }
}