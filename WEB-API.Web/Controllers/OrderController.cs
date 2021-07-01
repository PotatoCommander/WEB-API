using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Business.BusinessModels;
using WEB_API.Business.Interfaces;
using WEB_API.DAL.Models.Enums;
using WEB_API.Web.ViewModels;

namespace WEB_API.Web.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : BaseController
    {
        private IOrderService _orderService;
        private IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost("addToOrder")]
        [AllowAnonymous]
        public async Task<ActionResult> AddToOrder(OrderDetailViewModel orderDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                var orderDetail = _mapper.Map<OrderDetailModel>(orderDetailViewModel);
                if (User.Identity != null && User.Identity.IsAuthenticated)
                {
                    return await AddToUserOrder(orderDetail);
                }

                return await AddToAnonymousOrder(orderDetail);
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }



        public async Task<ActionResult> RemoveFromOrder()
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult> ExecuteOrder()
        {
            throw new NotImplementedException();
        }
        private async Task<ActionResult> AddToUserOrder(OrderDetailModel orderDetail)
        {
            OrderModel result;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userOrder = await _orderService.GetOrderByUserId(userId);
            if (userOrder != null)
            {
                orderDetail.OrderId = userOrder.Id;
                result = await _orderService.AddDetailToOrder(orderDetail);
            }

            else
            {
                var newOrder = new OrderModel() {ApplicationUserId = userId, OrderStatus = OrderStatuses.OPENED};

                var createdOrder = await _orderService.CreateOrder(newOrder);
                orderDetail.OrderId = createdOrder.Id;
                result = await _orderService.AddDetailToOrder(orderDetail);
            }

            return Ok(_mapper.Map<OrderViewModel>(result));
        }
        private async Task<ActionResult> AddToAnonymousOrder(OrderDetailModel orderDetail)
        {
            var orderId = Request.Cookies.ContainsKey("OrderId") ? Request.Cookies["OrderId"] : null;
            OrderModel result;
            if (orderId != null)
            {
                var order = await _orderService.GetOrderById(int.Parse(orderId));
                if (order != null)
                {
                    orderDetail.OrderId = order.Id;
                    result = await _orderService.AddDetailToOrder(orderDetail);
                    return Ok(_mapper.Map<OrderViewModel>(result));
                }

                ModelState.AddModelError("", "Order with ID from your cookie not found");
                return NotFound(GetModelStateErrors(ModelState));
            }

            var newOrder = new OrderModel() {ApplicationUserId = null, OrderStatus = OrderStatuses.OPENED};
            var createdOrder = await _orderService.CreateOrder(newOrder);
            Response.Cookies.Append("OrderId", createdOrder.Id.ToString());
            orderDetail.OrderId = createdOrder.Id;
            result = await _orderService.AddDetailToOrder(orderDetail);
            return Ok(result);
        }

        
    }
}