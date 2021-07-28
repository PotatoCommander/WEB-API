using System.Collections.Generic;
using WEB_API.Business.BusinessModels;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.Web.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ICollection<OrderDetailViewModel> OrderDetails { get; set; }
        public OrderStatuses OrderStatus { get; set; }
    }
}