using System.Threading.Tasks;
using WEB_API.Business.BusinessModels;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.Business.Interfaces
{
    public interface IOrderService
    {
        Task<OrderModel> CreateOrder(OrderModel order);
        Task<OrderModel> AddDetailToOrder(OrderDetailModel orderDetail);
        Task<OrderModel> RemoveDetailFromOrder(OrderDetailModel orderDetail);
        Task<OrderModel> GetOrderById(int id);
        Task<OrderModel> GetOrderByUserId(string userId);
        Task<OrderModel> SetOrderStatus(int orderId,OrderStatuses status);
    }
}