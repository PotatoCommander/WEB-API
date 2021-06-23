using System.Linq;
using System.Threading.Tasks;
using WEB_API.DAL.Models;

namespace WEB_API.DAL.Repositories
{
    public interface IRatingRepository
    {
        Task<Rating> Add(Rating item);
        Task<Rating> Update(Rating item);
        public bool IsExists(int productId, string userId);
        IQueryable<Rating> GetAll();
    }
}