using System.ComponentModel;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.DAL.ViewModels
{
    public class FilterProductViewModel
    {
        public SortStates SortBy { get; set; } = SortStates.None;
        public Genres Genre { get; set; } = Genres.None;
        public Categories Category { get; set; } = Categories.None;
        public AgeRatings AgeRating { get; set; } = AgeRatings.None;
        public string SearchString { get; set; } = null;

        public decimal? PriceFrom { get; set; } = null;
        public decimal? PriceTo { get; set; } = null;

        public float? RatingFrom { get; set; } = null;

        public int? YearFrom { get; set; } = null;
        public int? YearTo { get; set; } = null;
        
        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
    }
}