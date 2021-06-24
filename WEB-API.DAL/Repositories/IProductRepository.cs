using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.DAL.Models;

namespace WEB_API.DAL.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product item);
        Task<Product> UpdateProduct(Product item);
        Task<Product> DeleteProduct(int id);
        IQueryable<Product> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Rating> AddRating(Rating item);
        Task<Rating> UpdateRating(Rating item);
        bool IsRatingExists(int productId, string userId);

    }
}