using WEB_API.Business.BusinessModels;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.Business.Interfaces
{
    public interface IOrderService
    {
        OrderDetailModel AddDetailToOrder(OrderDetailModel orderDetail);
        OrderDetailModel RemoveDetailFromOrder(OrderDetailModel orderDetail);
        OrderModel GetOrderById(int id);
        OrderModel SetOrderStatus(OrderStatuses status);
    }
}