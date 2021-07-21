using System.Threading.Tasks;
using WEB_API.Business.BusinessModels;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.Business.Interfaces
{
    public interface IOrderService
    {
        Task<OrderModel> CreateOrder(OrderModel order);
        Task<OrderModel> GetActiveOrder(string userId);
        Task<OrderModel> GetActiveOrder(int orderId);
        Task<OrderModel> ExecuteOrder(int orderId);
        Task<OrderModel> ExecuteOrder(string userId);
        Task<OrderModel> DiscardOrder(int orderId);
        Task<OrderModel> DiscardOrder(string orderId);
        
        
        Task<OrderDetailModel> AddDetailToOrder(OrderDetailModel orderDetail, string userId = null);
        Task<OrderDetailModel> RemoveDetailFromOrder(int orderId, int productId, uint? count);
        Task<OrderDetailModel> RemoveDetailFromOrder(string userId, int productId, uint? count);

        
    }
}