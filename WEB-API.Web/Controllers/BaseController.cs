using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WEB_API.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        public List<string> GetModelStateErrors(ModelStateDictionary modelState)
        {
            var errorList = new List<string>();
            foreach (var value in modelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    errorList.Add(error.ErrorMessage);
                }
            }

            return errorList;
        }
    }
}