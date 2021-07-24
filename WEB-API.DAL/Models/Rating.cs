namespace WEB_API.DAL.Models
{
    public class Rating
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public float ProductRating { get; set; }
    }
}