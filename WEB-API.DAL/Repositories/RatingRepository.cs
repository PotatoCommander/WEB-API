using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB_API.DAL.Data;
using WEB_API.DAL.Models;

namespace WEB_API.DAL.Repositories
{
    public class RatingRepository: IRepository<Rating>
    {
        private ApplicationDbContext _context;

        public RatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Rating> Add(Rating item)
        {
            var inserted = await _context.Ratings.AddAsync(item);
            await _context.SaveChangesAsync();
            return inserted.Entity;
        }

        public async Task<Rating> Update(Rating item)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Rating> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Rating> GetAll()
        {
            return _context.Ratings.AsNoTracking().AsQueryable();
        }

        public async Task<Rating> GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}