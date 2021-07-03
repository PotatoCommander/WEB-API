using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Business.BusinessModels;
using WEB_API.Business.Interfaces;
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

        [HttpPost("addDetail")]
        [AllowAnonymous]
        public async Task<ActionResult> AddToOrder(OrderDetailViewModel orderDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                var orderDetail = _mapper.Map<OrderDetailModel>(orderDetailViewModel);
                OrderModel result;
                if (User.Identity != null && User.Identity.IsAuthenticated)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    result = await _orderService.AddDetailToOrder(orderDetail, userId);
                    
                }
                else
                {
                    var orderId = Request.Cookies.ContainsKey("OrderId") ? Request.Cookies["OrderId"] : null;
                    if (orderId != null)
                    {
                        orderDetail.OrderId = Convert.ToInt32(orderId);
                    }

                    result = await _orderService.AddDetailToOrder(orderDetail);
                    if (result != null)
                    {
                        Response.Cookies.Append("OrderId", result.Id.ToString());
                    }
                }

                if (result != null)
                {
                    return Ok(_mapper.Map<OutOrderViewModel>(result));
                }
                
                ModelState.AddModelError("", "Error occurred while adding detail.");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }
    }
}