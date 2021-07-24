using System.Linq;
using System.Threading.Tasks;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.DAL.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<Order> GetActiveOrderById(int id);
        Task<Order> GetActiveOrderByUserId(string userId);
        Task<Order> UpdateOrderStatus(int orderId, OrderStatuses status);
        Task<Order> DeleteOrder(int orderId, bool revertDetails = false);
        Task<decimal> CalculateTotalPrice(int orderId);
        
        
        Task<Order> CreateOrderDetail(OrderDetail orderDetail);
        Task<OrderDetail> GetOrderDetail(int orderId, int productId);
        Task<Order> UpdateOrderDetail(OrderDetail orderDetail);
        Task<Order> DeleteOrderDetail(int productId, int orderId, uint? count);
        
    }
}