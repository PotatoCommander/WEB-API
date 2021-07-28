using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WEB_API.Business.BusinessModels;
using WEB_API.Business.Interfaces;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Enums;
using WEB_API.DAL.Models.Filters;
using WEB_API.DAL.Repositories;

namespace WEB_API.Business.Services
{
    public class ProductService: IProductService
    {
        private IProductRepository _repository;
        private IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ProductModel> AddProduct(ProductModel item)
        {
            var result = await _repository.AddProduct(_mapper.Map<Product>(item));
            return _mapper.Map<ProductModel>(result);
        }

        public async Task<ProductModel> UpdateProduct(ProductModel item)
        {
            var result = await _repository.UpdateProduct(_mapper.Map<Product>(item));
            return _mapper.Map<ProductModel>(result);
        }

        public async Task<ProductModel> DeleteProduct(int id)
        {
            var result = await _repository.DeleteProduct(id);
            return _mapper.Map<ProductModel>(result);
        }

        public async Task<ProductModel> GetProductById(int id)
        {
            var result = await _repository.GetProductById(id);
            return _mapper.Map<ProductModel>(result);
        }

        public async Task<RatingModel> SetProductRating(RatingModel rating)
        {
            Rating result;
            if (_repository.IsRatingExists(rating.ProductId, rating.ApplicationUserId))
            {
                result =  await _repository.UpdateRating(_mapper.Map<Rating>(rating));
                return _mapper.Map<RatingModel>(result);
            }

            result = await _repository.AddRating(_mapper.Map<Rating>(rating));
            return _mapper.Map<RatingModel>(result);
        }

        public async Task<List<ProductModel>> GetAllProducts()
        {
            var result = await _repository.GetAllProducts().ToListAsync();
            return _mapper.Map<List<ProductModel>>(result);
        }

        public async Task<List<ProductModel>> FilterProductsBy(ProductFilterModel filterModel)
        {
            var filter = _mapper.Map<ProductFilter>(filterModel);
            var query = FilterProducts(filter);
            query = SortProducts(filter, query);
            query = PaginationProduct(filter, query);
            var result = _mapper.Map<List<ProductModel>>(await query.ToListAsync());
            return result;
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
            var query = _repository.GetAllProducts();
            if (filter.Category != Categories.None)
            {
                query = query.Where(x => x.Category == filter.Category);
            }

            if (filter.Genre != Genres.None)
            {
                query = query.Where(x => x.Genre == filter.Genre);
            }

            if (filter.AgeRating != AgeRatings.None)
            {
                query = query.Where(x => x.AgeRating == filter.AgeRating);
            }

            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                query = query.Where(x => x.Name.ToLower(CultureInfo.InvariantCulture).Contains(filter.SearchString));
            }

            if (filter.RatingFrom != null)
            {
                query = query.Where(x => x.Rating > filter.RatingFrom);
            }

            if (filter.YearFrom != null)
            {
                query = query.Where(x => x.CreationTime.Year > filter.YearFrom);
            }

            if (filter.YearTo != null)
            {
                query = query.Where(x => x.CreationTime.Year < filter.YearTo);
            }

            return query;
        }
    }
}