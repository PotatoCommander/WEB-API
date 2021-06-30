using WEB_API.DAL.Models.Enums;

namespace WEB_API.Business.BusinessModels
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public OrderStatuses OrderStatus { get; set; }
    }
}