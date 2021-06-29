using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.DAL.Models
{
    public class Order
    {
        [Key]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public OrderStatuses OrderStatus { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}