using System.Linq;
using System.Threading.Tasks;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.DAL.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<Order> GetOrder(int id, bool loadNavFields, OrderStatuses status);
        Task<Order> GetOrder(string userId, bool loadNavFields, OrderStatuses status);
        Task<Order> UpdateOrderStatus(int orderId, OrderStatuses status);
        Task<Order> DeleteOrder(int orderId, bool revertDetails = false);


        Task<OrderDetail> CreateOrderDetail(OrderDetail orderDetail);
        Task<OrderDetail> GetOrderDetail(int orderId, int productId);
        Task<OrderDetail> UpdateOrderDetail(OrderDetail orderDetail);
        Task<OrderDetail> DeleteOrderDetail(int productId, int orderId, uint? count);
    }
}