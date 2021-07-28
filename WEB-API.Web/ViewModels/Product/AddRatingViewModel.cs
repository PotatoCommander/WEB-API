using System.ComponentModel.DataAnnotations;

namespace WEB_API.Web.ViewModels
{
    public class AddRatingViewModel
    {
        [Required(ErrorMessage = "Rating required")]
        public float ProductRating { get; set; }
        [Required(ErrorMessage = "ProductId required")]
        public int ProductId { get; set; }
    }
}