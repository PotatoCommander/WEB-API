namespace WEB_API.DAL.Models
{
    public class OrderDetail
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public uint Quantity { get; set; }
    }
}