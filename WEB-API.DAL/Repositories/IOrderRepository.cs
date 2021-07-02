using System.Threading.Tasks;
using WEB_API.DAL.Models;

namespace WEB_API.DAL.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<Order> DeleteOrder(int orderId);
        Task<Order> AddOrderDetail(OrderDetail orderDetail);
        Task<Order> DeleteOrderDetail(OrderDetail orderDetail);
        Task<Order> GetOrderById(int id);
        Task<Order> GetOrderByUserId(string userId);
        Task<bool> IsOrderExists(int orderId);
    }
}