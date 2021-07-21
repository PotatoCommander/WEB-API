using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB_API.DAL.Data;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Enums;

namespace WEB_API.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddOrder(Order order)
        {
            var result = await _context.Orders.AddAsync(order);
            if (result.State == EntityState.Added)
            {
                await _context.SaveChangesAsync();
                return result.Entity;
            }

            return null;
        }

        public async Task<Order> UpdateOrderStatus(int orderId, OrderStatuses status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.OrderStatus = status;
                await _context.SaveChangesAsync();
                return order;
            }

            return null;
        }

        public async Task<OrderDetail> GetOrderDetail(int orderId, int productId)
        {
            var detail = await _context.OrderDetails.AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
            return detail;
        }

        public async Task<OrderDetail> CreateOrderDetail(OrderDetail detail)
        {
            if (detail != null)
            {
                var product = await _context.Products.FindAsync(detail.ProductId);
                if (product != null)
                {
                    var insertedDetail = await _context.OrderDetails.AddAsync(detail);
                    if (product.Count >= detail.Quantity && insertedDetail.State == EntityState.Added)
                    {
                        product.Count -= detail.Quantity;
                        await _context.SaveChangesAsync();
                        return insertedDetail.Entity;
                    }
                }
            }

            return null;
        }

        public async Task<OrderDetail> UpdateOrderDetail(OrderDetail detail)
        {
            if (detail != null)
            {
                var product = await _context.Products.FindAsync(detail.ProductId);
                if (product != null)
                {
                    var detailToUpdate = await _context.OrderDetails.FindAsync(detail.ProductId, detail.OrderId);
                    if (detailToUpdate != null && product.Count >= detail.Quantity)
                    {
                        detailToUpdate.Quantity += detail.Quantity;
                        product.Count -= detail.Quantity;
                        await _context.SaveChangesAsync();
                        return detailToUpdate;
                    }
                }
            }

            return null;
        }

        public async Task<OrderDetail> DeleteOrderDetail(int productId, int orderId, uint? count)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                var detail = await _context.OrderDetails.FindAsync(productId, orderId);
                if (count != null)
                {
                    if (detail.Quantity > count)
                    {
                        detail.Quantity -= (uint) count;
                        product.Count += (uint) count;
                        await _context.SaveChangesAsync();
                        return detail;
                    }

                    return null;
                }

                var removedDetail = _context.OrderDetails.Remove(detail);
                product.Count += detail.Quantity;
                if (removedDetail.State == EntityState.Deleted)
                {
                    await _context.SaveChangesAsync();
                    return removedDetail.Entity;
                }

                return null;
            }

            return null;
        }

        public async Task<Order> DeleteOrder(int orderId, bool revertDetails = false)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return null;
            }

            if (revertDetails)
            {
                await _context.Entry(order).Collection(x => x.OrderDetails).LoadAsync();
                if (order.OrderDetails != null)
                {
                    //This is not a better way to delete i think
                    foreach (var orderDetail in order.OrderDetails)
                    {
                        var product = await _context.Products.FindAsync(orderDetail.ProductId);
                        if (product == null)
                        {
                            return null;
                        }

                        product.Count += orderDetail.Quantity;
                    }
                }
            }

            var deletedOrder = _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return deletedOrder?.Entity;
        }

        public async Task<Order> GetOrder(int id, bool loadNavFields, OrderStatuses status)
        {
            if (loadNavFields)
            {
                //experiment
                var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id && x.OrderStatus == status);
                await _context.Entry(order).Collection(x => x.OrderDetails).LoadAsync();
                return order;
            }

            return await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.OrderStatus == status);
        }

        public async Task<Order> GetOrder(string userId, bool loadNavFields, OrderStatuses status)
        {
            if (loadNavFields)
            {
                var order = await _context.Orders
                    .FirstOrDefaultAsync(x => x.ApplicationUserId == userId && x.OrderStatus == status);
                await _context.Entry(order).Collection(x => x.OrderDetails).LoadAsync();
                return order;
            }

            return await _context.Orders.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ApplicationUserId == userId && x.OrderStatus == status);
        }


        //Add profile publication
        //Exceptions log
        //IIS publication
        //Little fixes
        //Optimization
    }
}