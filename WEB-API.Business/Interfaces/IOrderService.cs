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

        Task<OrderModel> ExecuteOrder(int orderId);
        Task<OrderModel> ExecuteOrder(string userId);
        Task<OrderModel> DiscardOrder(int orderId);
        Task<OrderModel> DiscardOrder(string orderId);
    }
}