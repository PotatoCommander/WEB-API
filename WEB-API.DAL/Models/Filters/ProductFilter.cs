using System;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.DAL.Models.Filters
{
    public class ProductFilter
    {
        public SortStates SortBy { get; set; }
        public Genres Genre { get; set; }
        public Categories Category { get; set; }
        public AgeRatings AgeRating { get; set; }
        public string SearchString { get; set; }
        public int? RatingFrom { get; set; }
        
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
        
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}