using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB_API.DAL.Models;
using WEB_API.DAL.Repositories;
using WEB_API.Web.ViewModels;

namespace WEB_API.Web.Controllers
{
    [ApiController]
    [Route("api/rating")]
    public class RatingController : BaseController
    {
        private IRepository<Rating> _repository;
        private IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;

        public RatingController(IRepository<Rating> repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPost("AddRating")]
        public async Task<ActionResult> AddRating(AddRatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rating = _mapper.Map<Rating>(model);
                rating.ApplicationUserId = _userManager.GetUserId(User);
                var result = await _repository.Add(rating);
                return Ok(result);
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }
    }
}