using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB_API.DAL.Data;
using WEB_API.DAL.Models;

namespace WEB_API.DAL.Repositories
{
    public class RatingRepository: IRatingRepository
    {
        private ApplicationDbContext _context;

        public RatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //TODO; Fix naming
        public async Task<Rating> Add(Rating item)
        {
            var inserted =  await _context.Ratings.AddAsync(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return inserted.Entity;
        }

        public async Task<Rating> Update(Rating item)
        {
            var rating = _context.Ratings.Update(item);
            await _context.SaveChangesAsync();
            return rating.Entity;
        }

        public bool IsExists(int productId, string userId)
        {
            return _context.Ratings.AsNoTracking().Any(x => x.ProductId == productId 
                                                            && string.Equals(x.ApplicationUserId, userId));
        }
        public IQueryable<Rating> GetAll()
        {
            return _context.Ratings.AsNoTracking().AsQueryable();
        }
    }
}