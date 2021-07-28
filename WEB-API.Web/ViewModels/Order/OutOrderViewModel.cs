using System.Collections.Generic;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.Web.ViewModels
{
    public class OutOrderViewModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ICollection<OutOrderDetailViewModel> OrderDetails { get; set; }
        public OrderStatuses OrderStatus { get; set; }
        public decimal TotalSum { get; set; }
    }
}