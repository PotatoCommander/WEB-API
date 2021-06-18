using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WEB_API.DAL.Data;
using WEB_API.DAL.Models;

namespace WEB_API.DAL.Repositories
{
    public class ProductRepository: IRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Product item)
        {
            bool isSucceeded;
            try
            {
                _context.Products.Add(item);
                _context.SaveChanges();
                isSucceeded = true;
            }
            catch (DbUpdateException ex)
            {
                isSucceeded = false;
            }

            return isSucceeded;
        }
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }
    }
}