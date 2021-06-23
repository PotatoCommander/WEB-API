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
        private IRatingRepository _ratingRepository;
        private IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;

        public RatingController(IRatingRepository ratingRepository, IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("SetRating")]
        public async Task<ActionResult> SetRating(AddRatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rating = _mapper.Map<Rating>(model);
                rating.ApplicationUserId = _userManager.GetUserId(User);
                Rating result;
                //TODO: move to service
                if (_ratingRepository.IsExists(rating.ProductId, rating.ApplicationUserId))
                {
                    result = await _ratingRepository.Update(rating);
                    if (result != null)
                    {
                        return Ok(result);
                    }

                    ModelState.AddModelError("", "Updating error.");
                    return BadRequest(GetModelStateErrors(ModelState));
                }

                result = await _ratingRepository.Add(rating);
                if (result != null)
                {
                    return Ok(result);
                }

                ModelState.AddModelError("", "Creating error.");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }
    }
}