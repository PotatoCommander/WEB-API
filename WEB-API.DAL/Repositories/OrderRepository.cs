using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB_API.DAL.Data;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.DAL.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Order> AddOrder(Order order)
        {
            var result = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        //Returns the order with loaded nav fields
        public async Task<Order> GetOrderById(int id)
        {
           var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
           if (order != null)
           {
               await _context.OrderDetails.Where(x => x.OrderId == order.Id).LoadAsync();
           }
           
           return order;
        }

        public async Task<Order> GetOrderByUserId(string userId)
        {
            var order =  await _context.Orders.FirstOrDefaultAsync(x => x.ApplicationUserId == userId);
            if (order != null)
            {
                await _context.OrderDetails.Where(x => x.OrderId == order.Id).LoadAsync();
            }
            
            return order;
        }

        public async Task<Order> AddDetailToOrder(OrderDetail detail)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(detail.OrderId, detail.ProductId);
            if (orderDetail != null)
            {
                await _context.Products.Where(x => x.Id == orderDetail.ProductId).LoadAsync();
                orderDetail.Quantity += detail.Quantity;
                orderDetail.Product.Count -= detail.Quantity;
                await _context.SaveChangesAsync();
                return await GetOrderById(orderDetail.OrderId);
            }
            
            var result = await _context.OrderDetails.AddAsync(detail);
            await _context.SaveChangesAsync();
            return await GetOrderById(result.Entity.OrderId);
        }

        public async Task<Order> DeleteDetailFromOrder(int orderId, int productId)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(orderId, productId);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(new OrderDetail(){OrderId = orderId, ProductId = productId});
                await _context.SaveChangesAsync();
                return await GetOrderById(orderDetail.OrderId);
            }

            return null;
        }

        public async Task<Order> UpdateOrderStatus(int orderId, OrderStatuses orderStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.OrderStatus = orderStatus;
            }

            return order;
        }
    }
}