using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Filters;

namespace WEB_API.Business.Interfaces
{
    public interface IProductService
    {
        Task<Product> AddItem(Product item);
        Task<Product> UpdateItem(Product item);
        Task<Product> DeleteItem(int id);
        Task<Product> GetItemById(int id);
        Task<List<Product>> GetAllItems();
        Task<IQueryable<Product>> FilterBy(ProductFilter filter);
    }
}