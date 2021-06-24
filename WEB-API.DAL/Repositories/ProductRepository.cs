using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WEB_API.DAL.Data;
using WEB_API.DAL.Models;

namespace WEB_API.DAL.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
        }

        
        public async Task<Product> AddProduct(Product item)
        {
            item.CreationTime = DateTime.UtcNow;
            var inserted = await _context.Products.AddAsync(item);
            await _context.SaveChangesAsync();
            return inserted.Entity;
        }
        
        public IQueryable<Product> GetAllProducts()
        {
            return _context.Products.AsNoTracking().AsQueryable();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> UpdateProduct(Product item)
        {
            var product = _context.Products.Update(item);
            await _context.SaveChangesAsync();
            return product.Entity;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            
            return product;
            //TODO: Move rating to product
        }
        public async Task<Rating> AddRating(Rating item)
        {
            var inserted =  await _context.Ratings.AddAsync(item);
            await _context.SaveChangesAsync();
            return inserted.Entity;
        }

        public async Task<Rating> UpdateRating(Rating item)
        {
            var rating = _context.Ratings.Update(item);
            await _context.SaveChangesAsync();
            return rating.Entity;
        }

        public bool IsRatingExists(int productId, string userId)
        {
            return _context.Ratings.AsNoTracking().Any(x => x.ProductId == productId 
                                                            && string.Equals(x.ApplicationUserId, userId));
        }

        
    }
}