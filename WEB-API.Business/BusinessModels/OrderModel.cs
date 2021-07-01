using System.Collections.Generic;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.Business.BusinessModels
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ICollection<OrderDetailModel> OrderDetails { get; set; }
        public OrderStatuses OrderStatus { get; set; }
        public decimal TotalSum { get; set; }
    }
}