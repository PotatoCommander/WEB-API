using System.Collections.Generic;
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

        public async Task<OrderModel> GetActiveOrder(string userId)
        {
            var result = await _orderRepository.GetOrder(userId, true, OrderStatuses.OPENED);
            return result != null ? _mapper.Map<OrderModel>(result) : null;
        }

        public async Task<OrderModel> GetActiveOrder(int orderId)
        {
            var result = await _orderRepository.GetOrder(orderId, true, OrderStatuses.OPENED);
            return result != null ? _mapper.Map<OrderModel>(result) : null;
        }

        

        public async Task<OrderDetailModel> AddDetailToOrder(OrderDetailModel orderDetail, string userId = null)
        {
            Order order;
            if (userId != null)
            {
                order = await _orderRepository.GetOrder(userId, false, OrderStatuses.OPENED);
            }
            else
            {
                order = orderDetail.OrderId != null //make sure is it clean logic
                    ? await _orderRepository.GetOrder((int) orderDetail.OrderId, false, OrderStatuses.OPENED)
                    : null;
            }
            
            
            if (order != null)
            {
                orderDetail.OrderId = order.Id;
                OrderDetail result;
                if (await _orderRepository.GetOrderDetail(order.Id, orderDetail.ProductId) != null)
                {
                    result = await _orderRepository.UpdateOrderDetail(_mapper.Map<OrderDetail>(orderDetail));
                }
                else
                {
                    result = await _orderRepository.CreateOrderDetail(_mapper.Map<OrderDetail>(orderDetail));
                }
                return result != null ? _mapper.Map<OrderDetailModel>(result) : null;
            }

            var newOrder = new OrderModel() {OrderStatus = OrderStatuses.OPENED, ApplicationUserId = userId};
            var createdOrder = await _orderRepository.AddOrder(_mapper.Map<Order>(newOrder));
            if (createdOrder != null)
            {
                orderDetail.OrderId = createdOrder.Id;
                var result = await _orderRepository.CreateOrderDetail(_mapper.Map<OrderDetail>(orderDetail));
                return result != null ? _mapper.Map<OrderDetailModel>(result) : null;
            }

            return null;
        }
        
        public async Task<OrderDetailModel> RemoveDetailFromOrder(int orderId, int productId, uint? count)
        {
            var result = await _orderRepository.DeleteOrderDetail(productId, orderId, count);
            return result != null ? _mapper.Map<OrderDetailModel>(result) : null;
        }

        public async Task<OrderDetailModel> RemoveDetailFromOrder(string userId, int productId, uint? count)
        {
            var order = await _orderRepository.GetOrder(userId, false, OrderStatuses.OPENED);
            if (order != null)
            {
                return await RemoveDetailFromOrder(order.Id, productId, count);
            }

            return null;
        }

        public async Task<OrderModel> ExecuteOrder(int orderId)
        {
            var result = await _orderRepository.UpdateOrderStatus(orderId, status: OrderStatuses.EXECUTED);
            return result != null ? _mapper.Map<OrderModel>(result) : null;
        }

        public async Task<OrderModel> ExecuteOrder(string userId)
        {
            var order = await _orderRepository.GetOrder(userId,false, OrderStatuses.OPENED);
            if (order != null)
            {
                return await ExecuteOrder(order.Id);
            }

            return null;
        }

        public async Task<OrderModel> DiscardOrder(int orderId)
        {
            var result = await _orderRepository.DeleteOrder(orderId, revertDetails: true);
            return result != null ? _mapper.Map<OrderModel>(result)  : null;
        }

        public async Task<OrderModel> DiscardOrder(string userId)
        {
            var order = await _orderRepository.GetOrder(userId, false, OrderStatuses.OPENED);
            if (order != null)
            {
                return await DiscardOrder(order.Id);
            }

            return null;
        }

    }
}