namespace WEB_API.DAL.Models
{
    public class Rating
    {
        public string ApplicationUserId { get; set; }
        //TODO: attributtes
        public ApplicationUser ApplicationUser { get; set; }
        public int ProductId { get; set; }
        //Dont display
        public Product Product { get; set; }
        public float ProductRating { get; set; }
    }
}