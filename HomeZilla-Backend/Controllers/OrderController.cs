using Final.Authorization;
using Final.Entities;
using Final.Model.Order;
using Final.Repositories.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orderService;
        private readonly IJwtUtils _jwtUtils;
        public OrderController(IOrderRepo orderService, IJwtUtils jwtUtils)
        {
            _orderService = orderService;
            _jwtUtils = jwtUtils;
        }

        [HttpPost("/BookOrder"), Authorize(Role.Customer)]
        public async Task<IActionResult> BookOrder([FromBody] BookOrder OrderData)
        {

            var Response = await _orderService.BookOrder(OrderData, _jwtUtils.GetUserId(HttpContext)) ;
            return Ok(Response);
        }

        [HttpPut("/CancelOrder"), Authorize(Role.Customer)]
        public async Task<IActionResult> CancelOrder([FromBody] ChangeStatus Status)
        {
            var Response = await _orderService.CancelOrder(Status);
            return Ok(Response);
        }

        [HttpPut("/DeclineOrder"), Authorize(Role.Provider)]
        public async Task<IActionResult> DeclineOrder([FromBody] ChangeStatus Status)
        {
            var Response = await _orderService.DeclineOrder(Status);
            return Ok(Response);
        }

        [HttpPut("/AcceptOrder"), Authorize(Role.Provider)]
        public async Task<IActionResult> AcceptOrder([FromBody] ChangeStatus Status)
        {
            var Response = await _orderService.AcceptOrder(Status);
            return Ok(Response);
        }
    }
}
