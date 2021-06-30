﻿using System.Linq;
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

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<OrderDetail> AddDetailToOrder(OrderDetail detail)
        {
            var result = await _context.OrderDetails.AddAsync(detail);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<OrderDetail> DeleteDetailFromOrder(OrderDetail detail)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(detail);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(detail);
                await _context.SaveChangesAsync();
            }

            return orderDetail;
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

        public bool IsOrderExists(int id)
        {
            return _context.Orders.AsNoTracking().Any(x => x.Id == id && x.OrderStatus == OrderStatuses.IN_PROGRESS);
        }
    }
}