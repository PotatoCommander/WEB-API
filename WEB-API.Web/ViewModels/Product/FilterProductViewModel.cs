using WEB_API.DAL.Models.Enums;

namespace WEB_API.Web.ViewModels.Product
{
    public class FilterProductViewModel
    {
        public SortStates SortBy { get; set; } = SortStates.DateDesc;
        public Genres Genre { get; set; } = Genres.None;
        public Categories Category { get; set; } = Categories.None;
        public AgeRatings AgeRating { get; set; } = AgeRatings.None;
        public string SearchString { get; set; } = null;
        public float? RatingFrom { get; set; } = null;
        public int? YearFrom { get; set; } = null;
        public int? YearTo { get; set; } = null;
        
        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
    }
}