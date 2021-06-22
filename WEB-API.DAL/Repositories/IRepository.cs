using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.DAL.Models;

namespace WEB_API.DAL.Repositories
{
    public interface IRepository<T> where T: class
    {
        Task<T> Add(T item);
        Task<T> Update(T item);
        Task<T> Delete(int id);
        IQueryable<T> GetAll();
        Task<T> GetById(int id);

    }
}