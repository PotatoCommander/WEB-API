using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_API.DAL.Models;
using WEB_API.DAL.Repositories;
using WEB_API.DAL.ViewModels;
using WEB_API.Web.Helpers;

namespace WEB_API.Web.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : BaseController
    {
        private IRepository _repository;
        private IMapper _mapper;

        public ProductController(IRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        [HttpGet("GetAll")]
        [AllowAnonymous]
        public ActionResult GetAll()
        {
            try
            {
                var result = _repository.GetAll();
                return new JsonResult(result);
            }
            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError("","Exception occured whe getting data from DB.");
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }
        [HttpPost("AddProduct")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> AddProduct(AddProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Add(_mapper.Map<Product>(model));
                if (result != null)
                {
                    return new JsonResult(result);
                }
                ModelState.AddModelError("","An error occured when updating database.");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var deleted = await _repository.Delete(id);
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
                var result = await _repository.Update(_mapper.Map<Product>(model));
                if (result != null)
                {
                    return new JsonResult(result);
                }
                ModelState.AddModelError("","An error occured when updating database.");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }
        
    }
}