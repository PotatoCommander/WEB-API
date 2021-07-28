using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Business.BusinessModels;
using WEB_API.Business.Interfaces;
using WEB_API.DAL.Models;
using WEB_API.Web.Helpers;
using WEB_API.Web.ViewModels;

namespace WEB_API.Web.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : BaseController
    {
        private IMapper _mapper;
        private IProductService _productService;
        private UserManager<ApplicationUser> _userManager;

        public ProductController(IMapper mapper, IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            var result = await _productService.GetAllProducts();
            if (result != null)
            {
                return Ok(result);
            }
            
            return BadRequest(GetModelStateErrors(ModelState));
        }

        [HttpPost("AddProduct")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> AddProduct(AddProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.AddProduct(_mapper.Map<ProductModel>(model));
                if (result != null)
                {
                    return Ok(result);
                }

                ModelState.AddModelError("", "An error occured when updating database.");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }
        
        [HttpDelete("Delete")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProduct(id);
            if (deleted != null)
            {
                return new JsonResult(deleted);
            }

            ModelState.AddModelError("", "Requested item not found");
            return NotFound(GetModelStateErrors(ModelState));
        }

        [HttpPut("Update")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> UpdateProduct(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var itemToUpdate = await _productService.GetProductById((int)model.Id);
                if (itemToUpdate != null)
                {
                    var result = await _productService.UpdateProduct(_mapper.Map(model, itemToUpdate));
                    if (result != null)
                    {
                        return Ok(result);
                    }

                    ModelState.AddModelError("", "An error occured when updating database.");
                    return BadRequest(GetModelStateErrors(ModelState));  
                }
                
                ModelState.AddModelError("", "Item to update doesn't exists.");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }

        [HttpGet("GetFiltered")]
        [AllowAnonymous]
        public async Task<ActionResult> GetFiltered([FromQuery] FilterProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.FilterProductsBy(_mapper.Map<ProductFilterModel>(model));
                return Ok(result);
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }
        
        [Authorize]
        [HttpPost("SetRating")]
        public async Task<ActionResult> SetRating(AddRatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rating = _mapper.Map<RatingModel>(model);
                rating.ApplicationUserId = _userManager.GetUserId(User);
                var result = await _productService.SetProductRating(rating);
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