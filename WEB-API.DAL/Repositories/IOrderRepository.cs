using System.Threading.Tasks;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.DAL.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<Order> GetOrderById(int id);
        Task<Order> GetOrderByUserId(string userId);
        Task<OrderDetail> AddDetailToOrder(OrderDetail detail);
        Task<OrderDetail> DeleteDetailFromOrder(OrderDetail detail);
        Task<Order> UpdateOrderStatus(int orderId, OrderStatuses orderStatus);
    }
}