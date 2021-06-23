using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB_API.Business.Interfaces;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Enums;
using WEB_API.DAL.Models.Filters;
using WEB_API.DAL.Repositories;
// ReSharper disable EnforceIfStatementBraces

namespace WEB_API.Business.Services
{
    public class ProductService: IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddItem(Product item)
        {
            return await _productRepository.Add(item);
        }

        public async Task<Product> UpdateItem(Product item)
        {
            return await _productRepository.Update(item);
        }

        public async Task<Product> DeleteItem(int id)
        {
            return await _productRepository.Delete(id);
        }

        public async Task<Product> GetItemById(int id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<List<Product>> GetAllItems()
        {
            //TODO: return view models
            //TODO: return lists
            return await _productRepository.GetAll().ToListAsync();
        }

        public async Task<IQueryable<Product>> FilterBy(ProductFilter filter)
        {
            var query = FilterProducts(filter);

            query = SortProducts(filter, query);
            
            query = PaginationProduct(filter, query);

            return query;
        }

        private IQueryable<Product> PaginationProduct(ProductFilter filter, IQueryable<Product> query)
        {
            query = query.Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);
            return query;
        }

        private IQueryable<Product> SortProducts(ProductFilter filter, IQueryable<Product> query)
        {
            switch (filter.SortBy)
            {
                case SortStates.None:
                    break;
                case SortStates.DateAsc:
                    query = query.OrderBy(x => x.CreationTime);
                    break;
                case SortStates.DateDesc:
                    query = query.OrderByDescending(x => x.CreationTime);
                    break;
                case SortStates.NameAsc:
                    query = query.OrderBy(x => x.Name);
                    break;
                case SortStates.NameDesc:
                    query = query.OrderByDescending(x => x.Name);
                    break;
                case SortStates.PriceAsc:
                    query = query.OrderBy(x => x.Price);
                    break;
                case SortStates.PriceDesc:
                    query = query.OrderByDescending(x => x.Price);
                    break;
                case SortStates.CreationDateAsc:
                    query = query.OrderBy(x => x.CreationTime);
                    break;
                case SortStates.CreationDateDesc:
                    query = query.OrderByDescending(x => x.CreationTime);
                    break;
                case SortStates.RatingAsc:
                    query = query.OrderBy(x => x.Rating);
                    break;
                case SortStates.RatingDesc:
                    query = query.OrderByDescending(x => x.Rating);
                    break;
            }

            return query;
        }

        private IQueryable<Product> FilterProducts(ProductFilter filter)
        {
            var query = _productRepository.GetAll();
            if (filter.Category != Categories.None)
                query = query.Where(x => x.Category == filter.Category);
            if (filter.Genre != Genres.None)
                query = query.Where(x => x.Genre == filter.Genre);
            if (filter.AgeRating != AgeRatings.None)
                query = query.Where(x => x.AgeRating == filter.AgeRating);
            if (!string.IsNullOrEmpty(filter.SearchString))
                query = query.Where(x => x.Name.ToLower(CultureInfo.InvariantCulture).Contains(filter.SearchString));
            if (filter.RatingFrom != null)
                query = query.Where(x => x.Rating > filter.RatingFrom);
            if (filter.YearFrom != null)
                query = query.Where(x => x.CreationTime.Year > filter.YearFrom);
            if (filter.YearTo != null)
                query = query.Where(x => x.CreationTime.Year < filter.YearTo);
            return query;
        }
    }
}