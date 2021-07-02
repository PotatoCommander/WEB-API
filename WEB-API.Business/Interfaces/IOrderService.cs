using System.Threading.Tasks;
using WEB_API.Business.BusinessModels;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.Business.Interfaces
{
    public interface IOrderService
    {
        Task<OrderModel> CreateOrder(OrderModel order);
        Task<OrderModel> AddDetailToOrder(OrderDetailModel orderDetail, string userId = null);
        Task<OrderModel> RemoveDetailFromOrder(int orderId, int productId);
        Task<OrderModel> GetOrderById(int id);
        Task<OrderModel> SetOrderStatus(int orderId,OrderStatuses status);
    }
}