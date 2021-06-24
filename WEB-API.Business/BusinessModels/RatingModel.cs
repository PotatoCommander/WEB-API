using WEB_API.DAL.Models;

namespace WEB_API.Business.BusinessModels
{
    public class RatingModel
    {
        public string ApplicationUserId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public float ProductRating { get; set; }
    }
}