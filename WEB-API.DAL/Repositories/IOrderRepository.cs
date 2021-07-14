using System.Threading.Tasks;
using WEB_API.DAL.Models;

namespace WEB_API.DAL.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<Order> DeleteOrder(int orderId);
        Task<Order> AddOrderDetail(OrderDetail orderDetail);
        Task<Order> DeleteOrderDetail(int productId, int orderId);
        Task<Order> GetOrderById(int id);
        Task<Order> GetOrderByUserId(string userId);
        Task<decimal> CalculateTotalPrice(int orderId);
        Task<bool> IsOrderExists(int orderId);
    }
}