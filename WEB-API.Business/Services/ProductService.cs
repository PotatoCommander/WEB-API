using System;
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
    public class ProductService: IDomainService
    {
        private IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<IQueryable<Product>> FilterBy(ProductFilter filter)
        {
            var query = _repository.GetAll();
            if (filter.Category!= Categories.None)
                query = query.Where(x => x.Category == filter.Category);
            if (filter.Genre != Genres.None)
                query = query.Where(x => x.Genre == filter.Genre);
            if (filter.AgeRating != AgeRatings.None)
                query = query.Where(x => x.AgeRating == filter.AgeRating);
            if (!string.IsNullOrEmpty(filter.SearchString))
                query = query.Where(x => x.Name.ToLower(CultureInfo.InvariantCulture).Contains(filter.SearchString));
            if (filter.PriceFrom != null)
                query = query.Where(x => x.Price > filter.PriceFrom);
            if (filter.PriceTo != null)
                query = query.Where(x => x.Price < filter.PriceTo);
            if (filter.RatingFrom != null)
                query = query.Where(x => x.Rating > filter.RatingFrom);
            if (filter.YearFrom != null)
                query = query.Where(x => x.DateOfProduction.Year > filter.YearFrom);
            if (filter.YearTo != null)
                query = query.Where(x => x.DateOfProduction.Year < filter.YearTo);

            switch (filter.SortBy)
            {
                case SortStates.None:
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
                case SortStates.DateOfProductionAsc:
                    query = query.OrderBy(x => x.DateOfProduction);
                    break;
                case SortStates.DateOfProductionDesc:
                    query = query.OrderByDescending(x => x.DateOfProduction);
                    break;
                case SortStates.RatingAsc:
                    query = query.OrderBy(x => x.Rating);
                    break;
                case SortStates.RatingDesc:
                    query = query.OrderByDescending(x => x.Rating);
                    break;
                //TODO: Remove from to price;
                //TODO: Only ascending
            }
            
            query = query.Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);

            return query;
        }
    }
}