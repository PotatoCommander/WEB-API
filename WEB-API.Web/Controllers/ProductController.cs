using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_API.DAL.Repositories;

namespace WEB_API.Web.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : BaseController
    {
        private IRepository _repository;

        public ProductController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("GetAll")]
        [AllowAnonymous]
        public ActionResult Index()
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
    }
}