using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB_API.DAL.Data;
using WEB_API.DAL.Models;

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
                    detail.Price = await CalculateDetailPrice(detail);
                    await _context.SaveChangesAsync();
                    return order;
                }

                return null;
            }

            return null;
        }

        private async Task<Order> UpdateOrderDetail(OrderDetail detail, Order order)
        {
            var detailToUpdate = order.OrderDetails.FirstOrDefault(x => x.ProductId == detail.ProductId);
            var product = _context.Products.FirstOrDefault(x => x.Id == detail.ProductId);
            if (detailToUpdate != null)
            {
                if (product != null && product.Count >= detail.Quantity)
                {
                    detailToUpdate.Quantity += detail.Quantity;
                    detailToUpdate.Price = await CalculateDetailPrice(detailToUpdate);
                    product.Count -= detail.Quantity;
                    await _context.SaveChangesAsync();
                    return order;
                }

                return null;
            }

            return null;
        }

        public async Task<Order> DeleteOrderDetail(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> DeleteOrder(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order != null)
            {
                var deletedOrder = _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return deletedOrder.Entity;
            }

            return null;
        }

        //Returns the order with loaded nav fields
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

        private async Task<decimal> CalculateDetailPrice(OrderDetail detail)
        {
            return (await _context.Products.FirstOrDefaultAsync(x => x.Id == detail.ProductId)).Price * detail.Quantity;
        }
        public async Task<bool> IsOrderExists(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId && x.OrderStatus == 0);
            return order != null;
        }
        
    }
}