using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Filters;

namespace WEB_API.Business.Interfaces
{
    public interface IDomainService
    {
        Task<IQueryable<Product>> FilterBy(ProductFilter filter);
    }
}