using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Business.Interfaces;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Filters;
using WEB_API.DAL.Repositories;
using WEB_API.Web.Helpers;
using WEB_API.Web.ViewModels;

namespace WEB_API.Web.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : BaseController
    {
        private IRepository _repository;
        private IMapper _mapper;
        private IDomainService _productService;

        public ProductController(IRepository repository, IMapper mapper, IDomainService productService)
        {
            _mapper = mapper;
            _repository = repository;
            _productService = productService;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public ActionResult GetAll()
        {
            var result = _repository.GetAll();
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
                var result = await _repository.Add(_mapper.Map<Product>(model));
                if (result != null)
                {
                    return Ok(result);
                }

                ModelState.AddModelError("", "An error occured when updating database.");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }

        //DEBUG------------------------------------------------------------------------
        [HttpPost("AddProducts")]
        public async Task<ActionResult> AddProducts(List<AddProductViewModel> model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model)
                {
                    await _repository.Add(_mapper.Map<Product>(item));
                }

                return Ok();
            }

            return BadRequest();
        }
        //DEBUG------------------------------------------------------------------------

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

                ModelState.AddModelError("", "An error occured when updating database.");
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
                var result = await _productService.FilterBy(_mapper.Map<ProductFilter>(model));
                return Ok(result.ToList());
            }

            return BadRequest();
        }
    }
}