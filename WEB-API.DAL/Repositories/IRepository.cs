using System.Collections;
using System.Collections.Generic;
using WEB_API.DAL.Models;

namespace WEB_API.DAL.Repositories
{
    public interface IRepository
    {
        bool Add(Product item);
        IEnumerable<Product> GetAll();
    }
}