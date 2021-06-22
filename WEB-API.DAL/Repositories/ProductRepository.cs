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
    public class ProductRepository: IRepository<Product>
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
        }

        
        public async Task<Product> Add(Product item)
        {
            item.CreationTime = DateTime.UtcNow;
            var inserted = await _context.Products.AddAsync(item);
            await _context.SaveChangesAsync();
            return inserted.Entity;
        }
        
        public IQueryable<Product> GetAll()
        {
            return _context.Products.AsNoTracking().AsQueryable();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> Update(Product item)
        {
            var product = _context.Products.Update(item);
            await _context.SaveChangesAsync();
            return product.Entity;
        }

        public async Task<Product> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            
            return product;
        }

        
    }
}