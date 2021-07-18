﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using WEB_API.Business.BusinessModels;
using WEB_API.Business.Interfaces;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Enums;
using WEB_API.DAL.Repositories;

namespace WEB_API.Business.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderModel> CreateOrder(OrderModel order)
        {
            var result = await _orderRepository.AddOrder(_mapper.Map<Order>(order));
            return _mapper.Map<OrderModel>(result);
        }

        public async Task<OrderModel> AddDetailToOrder(OrderDetailModel orderDetail, string userId = null)
        {
            Order order;
            if (userId != null)
            {
                order = await _orderRepository.GetOrderByUserId(userId);
            }
            else
            {
                order = orderDetail.OrderId != null //make sure is it clean logic
                    ? await _orderRepository.GetOrderById((int) orderDetail.OrderId)
                    : null;
            }
            
            if (order != null)
            {
                orderDetail.OrderId = order.Id;
                var result = await _orderRepository.AddOrderDetail(_mapper.Map<OrderDetail>(orderDetail));
                return result != null ? await AddTotalPriceToOrder(result) : null;
            }

            var newOrder = new OrderModel() {OrderStatus = OrderStatuses.OPENED, ApplicationUserId = userId};

            var createdOrder = await _orderRepository.AddOrder(_mapper.Map<Order>(newOrder));
            if (createdOrder != null)
            {
                orderDetail.OrderId = createdOrder.Id;
                var result = await _orderRepository.AddOrderDetail(_mapper.Map<OrderDetail>(orderDetail));
                return result != null ? await AddTotalPriceToOrder(result) : null;
            }

            return null;
        }

        private async Task<OrderModel> AddTotalPriceToOrder(Order result)
        {
            var outResult = _mapper.Map<OrderModel>(result);
            outResult.TotalSum = await _orderRepository.CalculateTotalPrice(outResult.Id);
            return outResult;
        }

        public async Task<OrderModel> RemoveDetailFromOrder(int orderId, int productId, uint? count)
        {
            var result = await _orderRepository.DeleteOrderDetail(productId, orderId, count);
            return result != null ? _mapper.Map<OrderModel>(result) : null;
        }

        public async Task<OrderModel> RemoveDetailFromOrder(string userId, int productId, uint? count)
        {
            var order = await _orderRepository.GetOrderByUserId(userId);
            if (order != null)
            {
                return await RemoveDetailFromOrder(order.Id, productId, count);
            }

            return null;
        }
        

        public async Task<OrderModel> SetOrderStatus(int orderId, int status)
        {
            if (Enum.IsDefined(typeof(OrderStatuses), status))
            {
                var result = await _orderRepository.UpdateOrderStatus(orderId,(OrderStatuses)status);
                return result != null ? _mapper.Map<OrderModel>(result) : null;
            }

            return null;
        }

        public async Task<OrderModel> SetOrderStatus(string userId, int status)
        {
            var order = await _orderRepository.GetOrderByUserId(userId);
            return await SetOrderStatus(order.Id, status);
        }
    }
}