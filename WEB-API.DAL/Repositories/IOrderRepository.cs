using System.Threading.Tasks;
using WEB_API.DAL.Models;

namespace WEB_API.DAL.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<Order> GetOrderById(int id);
        Task<OrderDetail> AddDetailToOrder(OrderDetail detail);
        Task<OrderDetail> DeleteDetailFromOrder(OrderDetail detail);
    }
}