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
        Task<Order> AddDetailToOrder(OrderDetail detail);
        Task<Order> DeleteDetailFromOrder(int orderId, int productId);
        Task<Order> UpdateOrderStatus(int orderId, OrderStatuses orderStatus);
    }
}