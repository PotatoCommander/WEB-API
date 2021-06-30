using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WEB_API.Business.BusinessModels;
using WEB_API.Business.Interfaces;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Enums;
using WEB_API.DAL.Repositories;

namespace WEB_API.Business.Services
{
    public class OrderService: IOrderService
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
            var result =  await _orderRepository.AddOrder(_mapper.Map<Order>(order));
            return _mapper.Map<OrderModel>(result);
        }

        public async Task<OrderModel> AddDetailToOrder(OrderDetailModel orderDetail)
        {
            await _orderRepository.AddDetailToOrder(_mapper.Map<OrderDetail>(orderDetail));
            var order = await _orderRepository.GetOrderById(orderDetail.OrderId);
            return _mapper.Map<OrderModel>(order);
        }

        public async Task<OrderModel> RemoveDetailFromOrder(OrderDetailModel orderDetail)
        {
            await _orderRepository.DeleteDetailFromOrder(_mapper.Map<OrderDetail>(orderDetail));
            var order = await _orderRepository.GetOrderById(orderDetail.OrderId);
            return _mapper.Map<OrderModel>(order);
        }

        public async Task<OrderModel> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            return _mapper.Map<OrderModel>(order);
        }

        public async Task<OrderModel> SetOrderStatus(int orderId,OrderStatuses status)
        {
            var order = await _orderRepository.UpdateOrderStatus(orderId, status);
            return _mapper.Map<OrderModel>(order);
        }

        public bool IsUserHaveOpenedOrder(string userId)
        {
            return _orderRepository.IsOrderExists(userId);
        }

        public bool IsOrderExists(int id)
        {
            return _orderRepository.IsOrderExists(id);
        }
    }
}