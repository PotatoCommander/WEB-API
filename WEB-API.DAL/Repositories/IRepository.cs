using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.DAL.Models;

namespace WEB_API.DAL.Repositories
{
    public interface IRepository
    {
        Task<Product> Add(Product item);
        Task<Product> Update(Product item);
        Task<Product> Delete(int id);
        IQueryable<Product> GetAll();
        Task<Product> GetById(int id);

    }
}