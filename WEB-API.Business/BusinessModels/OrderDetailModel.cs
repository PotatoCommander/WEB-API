namespace WEB_API.Business.BusinessModels
{
    public class OrderDetailModel
    {
        public int ProductId { get; set; }
        public int? OrderId { get; set; }
        public int Quantity { get; set; }
    }
}