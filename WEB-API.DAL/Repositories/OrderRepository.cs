using System.Collections.ObjectModel;
using System.Linq;
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
            await _context.SaveChangesAsync();
            return result.Entity;
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

        public async Task<Order> CreateOrderDetail(OrderDetail detail)
        {
            if (detail != null)
            {
                var product = await _context.Products.FindAsync(detail.ProductId);
                var insertedDetail = await _context.OrderDetails.AddAsync(detail);
                if (insertedDetail != null)
                {
                    if (product != null && product.Count >= detail.Quantity)
                    {
                        product.Count -= detail.Quantity;
                        var outOrder = await _context.Orders
                            .FirstOrDefaultAsync(x => x.Id == insertedDetail.Entity.OrderId);
                        if (outOrder != null)
                        {
                            await _context.SaveChangesAsync();
                            return await LoadDetailsToOrder(outOrder);
                        }
                    }
                }
            }

            return null;
        }

        public async Task<Order> UpdateOrderDetail(OrderDetail detail)
        {
            if (detail != null)
            {
                var detailToUpdate = await _context.OrderDetails
                    .FirstOrDefaultAsync(x => x.OrderId == detail.OrderId && x.ProductId == detail.ProductId);
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == detail.ProductId);
                if (detailToUpdate != null)
                {
                    if (product != null && product.Count >= detail.Quantity)
                    {
                        detailToUpdate.Quantity += detail.Quantity;
                        product.Count -= detail.Quantity;
                        var outOrder = await _context.Orders
                            .FirstOrDefaultAsync(x => x.Id == detailToUpdate.OrderId);
                        if (outOrder != null)
                        {
                            await _context.SaveChangesAsync();
                            return await LoadDetailsToOrder(outOrder);
                        }

                        return null;
                    }

                    return null;
                }

                return null;
            }

            return null;
        }

        public async Task<Order> DeleteOrderDetail(int productId, int orderId, uint? count)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            var detail = await _context.OrderDetails
                .FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
            if (detail != null && product != null)
            {
                if (count != null)
                {
                    if (detail.Quantity > count)
                    {
                        detail.Quantity -= (uint) count;
                        product.Count += (uint) count;
                        var outOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
                        if (outOrder != null)
                        {
                            await _context.SaveChangesAsync();
                            return await LoadDetailsToOrder(outOrder);
                        }

                        return null;
                    }

                    return null;
                }

                var removedDetail = _context.OrderDetails.Remove(detail);
                product.Count += detail.Quantity;
                if (removedDetail != null)
                {
                    var outOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
                    if (outOrder != null)
                    {
                        await _context.SaveChangesAsync();
                        return await LoadDetailsToOrder(outOrder);
                    }

                    return null;
                }

                return null;
            }

            return null;
        }

        public async Task<Order> DeleteOrder(int orderId, bool revertDetails = false)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order == null)
            {
                return null;
            }
            
            if (revertDetails)
            {
                order = await LoadDetailsToOrder(order, isTracking: true);
                if (order.OrderDetails != null)
                {
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

        public async Task<Order> GetActiveOrderById(int id)
        {
            return await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.OrderStatus == 0);
        }

        public async Task<Order> GetActiveOrderByUserId(string userId)
        {
            return await _context.Orders.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ApplicationUserId == userId && x.OrderStatus == 0);
        }

        private async Task<Order> LoadDetailsToOrder(Order order, bool isTracking = false)
        {
            if (order != null)
            {
                if (isTracking)
                {
                    await _context.OrderDetails.Where(x => x.OrderId == order.Id).LoadAsync();
                    order.OrderDetails ??= new Collection<OrderDetail>();
                }
                else
                {
                    await _context.OrderDetails.AsNoTracking().Where(x => x.OrderId == order.Id).LoadAsync();
                    order.OrderDetails ??= new Collection<OrderDetail>();
                }
            }

            return order;
        }

        public async Task<decimal> CalculateTotalPrice(int orderId)
        {
            return await _context.OrderDetails.Where(x => x.OrderId == orderId)
                .SumAsync(x => x.Product.Price * x.Quantity);
        }
        
        //Logging SeriLog (or something else). User actions. Warning logs. Error logs. File for each action.
        //GZIP as no tracking everywhere possible
        //Add profile publication
        //Exceptions log
        //IIS publication
        //Little fixes
        //Optimization
    }
}