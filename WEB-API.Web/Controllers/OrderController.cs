using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        [HttpGet("getOrder")]
        [AllowAnonymous]
        public async Task<ActionResult> GetOrder()
        {
            OrderModel result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                result = await _orderService.GetActiveOrder(userId);
            }
            else
            {
                var orderId = Request.Cookies.ContainsKey("OrderId") ? Request.Cookies["OrderId"] : null;
                if (orderId != null)
                {
                    result = await _orderService.GetActiveOrder(Convert.ToInt32(orderId));
                }
                else
                {
                    ModelState.AddModelError("", "Invalid OrderId or empty cookie.");
                    return BadRequest(GetModelStateErrors(ModelState));
                }
            }

            if (result != null)
            {
                return Ok(_mapper.Map<OutOrderViewModel>(result));
            }
                
            ModelState.AddModelError("", "Error occurred while adding detail.");
            return BadRequest(GetModelStateErrors(ModelState));
        }
        [HttpPost("addDetail")]
        [AllowAnonymous]
        public async Task<ActionResult> AddToOrder(OrderDetailViewModel orderDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                var orderDetail = _mapper.Map<OrderDetailModel>(orderDetailViewModel);
                OrderDetailModel result;
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
                        Response.Cookies.Append("OrderId", result.OrderId.ToString());
                    }
                }

                if (result != null)
                {
                    return Ok(_mapper.Map<OutOrderDetailViewModel>(result));
                }
                
                ModelState.AddModelError("", "Error occurred while adding detail.");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            return BadRequest(GetModelStateErrors(ModelState));
        }

        [HttpDelete("deleteDetail")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteOrderDetail(int productId, uint? count = null)
        {
            OrderDetailModel result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                result = await _orderService.RemoveDetailFromOrder(userId, productId, count);
            }
            else
            {
                var orderId = Request.Cookies.ContainsKey("OrderId") ? Request.Cookies["OrderId"] : null;
                if (orderId != null)
                {
                    result = await _orderService.RemoveDetailFromOrder(Convert.ToInt32(orderId), productId, count);
                }
                else
                {
                    ModelState.AddModelError("", "Cookie doesn't contains orderId");
                    return BadRequest(GetModelStateErrors(ModelState));
                }
            }

            if (result != null)
            {
                return Ok(_mapper.Map<OutOrderDetailViewModel>(result));
            }
            
            ModelState.AddModelError("", "Error occured while deleting order detail.");
            return BadRequest(GetModelStateErrors(ModelState));
        }

        [HttpPatch("ExecuteOrder")]
        [AllowAnonymous]
        public async Task<ActionResult> ExecuteOrder()
        {
            OrderModel result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                result = await _orderService.ExecuteOrder(userId);
            }
            else
            {
                var orderId = Request.Cookies.ContainsKey("OrderId") ? Request.Cookies["OrderId"] : null;
                if (orderId != null)
                {
                    result = await _orderService.ExecuteOrder(Convert.ToInt32(orderId));
                }
                else
                {
                    ModelState.AddModelError("", "Cookie doesn't contains orderId");
                    return BadRequest(GetModelStateErrors(ModelState));
                }
            }

            if (result != null)
            {
                return Ok(_mapper.Map<OutOrderViewModel>(result));
            }

            ModelState.AddModelError("", "Error occured while executing order.");
            return BadRequest(GetModelStateErrors(ModelState));
        }
        
        [HttpDelete("DiscardOrder")]
        [AllowAnonymous]
        public async Task<ActionResult> DiscardOrder()
        {
            OrderModel result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                result = await _orderService.DiscardOrder(userId);
            }
            //TODO: delete anonymous orders after 24 hours
            //TODO:Manual test endpoints
            //TODO: Write unit tests to one of request-chain
            //TODO: Caching
            else
            {
                var orderId = Request.Cookies.ContainsKey("OrderId") ? Request.Cookies["OrderId"] : null;
                if (orderId != null)
                {
                    result = await _orderService.DiscardOrder(Convert.ToInt32(orderId));
                }
                else
                {
                    ModelState.AddModelError("", "Cookie doesn't contains orderId");
                    return BadRequest(GetModelStateErrors(ModelState));
                }
            }

            if (result != null)
            {
                return Ok(_mapper.Map<OutOrderViewModel>(result));
            }

            ModelState.AddModelError("", "Error occured while discarding order.");
            return BadRequest(GetModelStateErrors(ModelState));
        }
    }
}