namespace WEB_API.DAL.Models
{
    public class OrderDetail
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}