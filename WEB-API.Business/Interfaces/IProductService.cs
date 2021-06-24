using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Business.BusinessModels;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Filters;

namespace WEB_API.Business.Interfaces
{
    public interface IProductService
    {
        Task<ProductModel> AddProduct(ProductModel item);
        Task<ProductModel> UpdateProduct(ProductModel item);
        Task<ProductModel> DeleteProduct(int id);
        Task<ProductModel> GetProductById(int id);
        Task<RatingModel> SetProductRating(RatingModel rating);
        Task<List<ProductModel>> GetAllProducts();
        Task<List<ProductModel>> FilterProductsBy(ProductFilterModel filter);
    }
}