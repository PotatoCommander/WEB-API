using System.Threading.Tasks;
using WEB_API.Business.BusinessModels;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.Business.Interfaces
{
    public interface IOrderService
    {
        Task<OrderModel> CreateOrder(OrderModel order);
        Task<OrderModel> AddDetailToOrder(OrderDetailModel orderDetail, string userId = null);
        Task<OrderModel> RemoveDetailFromOrder(int orderId, int productId, uint? count);
        Task<OrderModel> RemoveDetailFromOrder(string userId, int productId, uint? count);
        Task<OrderModel> SetOrderStatus(int orderId, int status);
        Task<OrderModel> SetOrderStatus(string userId, int status);
    }
}