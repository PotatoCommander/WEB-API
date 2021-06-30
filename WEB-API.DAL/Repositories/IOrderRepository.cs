using WEB_API.DAL.Models;

namespace WEB_API.DAL.Repositories
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order order);
        Order GetOrderById(int id);
        OrderDetail AddDetailToOrder(OrderDetail detail);
        OrderDetail DeleteDetailFromOrder(OrderDetail detail);
    }
}