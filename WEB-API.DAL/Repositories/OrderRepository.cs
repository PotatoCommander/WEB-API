using System;
using System.Collections.Generic;
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

        public async Task<Order> AddOrderDetail(OrderDetail orderDetail)
        {
            if (orderDetail != null)
            {
                var order = await GetOrderById(orderDetail.OrderId);
                if (order != null)
                {
                    Order result;
                    if (await IsOrderDetailExist(orderDetail.OrderId, orderDetail.ProductId))
                    {
                        result = await UpdateOrderDetail(orderDetail, order);
                    }
                    else
                    {
                        result = await CreateOrderDetail(orderDetail, order);
                    }

                    if (result != null)
                    {
                        return result;
                    }
                    
                }

                return null;

            }

            return null;
        }

        private async Task<bool> IsOrderDetailExist(int orderId, int productId)
        {
            var detail = await _context.OrderDetails.FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
            return detail != null;
        }

        private async Task<Order> CreateOrderDetail(OrderDetail detail, Order order)
        {
            var product = await _context.Products.FindAsync(detail.ProductId);
            if (order != null)
            {
                order.OrderDetails.Add(detail);
                if (product != null && product.Count >= detail.Quantity)
                {
                    product.Count -= detail.Quantity;
                    await _context.SaveChangesAsync();
                    return order;
                }

                return null;
            }

            return null;
        }
        //TODO: Fix count of products 

        private async Task<Order> UpdateOrderDetail(OrderDetail detail, Order order)
        {
            var detailToUpdate = order.OrderDetails.FirstOrDefault(x => x.ProductId == detail.ProductId);
            var product = _context.Products.FirstOrDefault(x => x.Id == detail.ProductId);
            if (detailToUpdate != null)
            {
                if (product != null && product.Count >= detail.Quantity)
                {
                    detailToUpdate.Quantity += detail.Quantity;
                    product.Count -= detail.Quantity;
                    await _context.SaveChangesAsync();
                    return order;
                }

                return null;
            }

            return null;
        }

        public async Task<Order> DeleteOrderDetail(int productId, int orderId, uint? count)
        {
            var order = await GetOrderById(orderId);
            //Should be Count in products and price in order detail DB-calculated? Will it make execution faster?
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (order != null)
            {
                var detail = order.OrderDetails.FirstOrDefault(x => x.ProductId == productId);
                if (detail != null && product != null)
                {
                    if (count != null)
                    {
                        if (detail.Quantity > count)
                        {
                            detail.Quantity -= (uint)count;
                            product.Count += (uint)count;
                            await _context.SaveChangesAsync();
                            return order;
                        }

                        return null;
                    }
                    
                    var isRemoved = order.OrderDetails.Remove(detail);
                    product.Count += detail.Quantity;
                    if (isRemoved)
                    {
                        await _context.SaveChangesAsync();
                        return order;
                    }

                    return null;
                }

                return null;
            }
            
            return null;
        }

        public async Task<Order> DeleteOrder(int orderId)
        {
            var order = await GetOrderById(orderId);
            if (order != null)
            {
                var deletedOrder = _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return deletedOrder.Entity;
            }

            return null;
        }

        //Returns the order with loaded nav fields
        //TODO: dont call repo methods from repo
        public async Task<Order> GetOrderById(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id && x.OrderStatus == 0);
            if (order != null)
            {
                await _context.OrderDetails.Where(x => x.OrderId == order.Id).LoadAsync();
                order.OrderDetails ??= new Collection<OrderDetail>();
            }

            return order;
        }

        public async Task<Order> GetOrderByUserId(string userId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x =>
                x.ApplicationUserId == userId && x.OrderStatus == 0);
            if (order != null)
            {
                await _context.OrderDetails.Where(x => x.OrderId == order.Id).LoadAsync();
                order.OrderDetails ??= new Collection<OrderDetail>();
            }
            
            return order;
        }

        public async Task<decimal> CalculateTotalPrice(int orderId)
        {
            return await _context.OrderDetails.Where(x => x.OrderId == orderId)
                .SumAsync(x => x.Product.Price * x.Quantity);
        }
        //TODO: Wrap select in Discard order and Apply order
        //Logging SeriLog (or something else). User actions. Warning logs. Error logs. File for each action.
        //GZIP as no tracking everywhere possible
        //Exceptions log
        //IIS publication
        //Little fixes
        //Optimization
    }
}