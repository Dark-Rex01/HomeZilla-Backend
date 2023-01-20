using Final.Authorization;
using Final.Entities;
using HomeZilla_Backend.Models.Analytics;
using HomeZilla_Backend.Repositories.Analytics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeZilla_Backend.Controllers
{
    [Route("api/Providers/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsRepo _analyticsRepo;
        private readonly IJwtUtils _jwtUtils;
        public AnalyticsController(IAnalyticsRepo analyticsRepo, IJwtUtils jwtUtils)
        {
            _analyticsRepo = analyticsRepo;
            _jwtUtils= jwtUtils;
        }
        [HttpGet("Get-All-Orders-Count"),Authorize(Role.Provider)]
        public async Task<ActionResult<int>> GetAllOrdersCount()
        {
            var response = await _analyticsRepo.GetTotalOrders(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }
        [HttpGet("Get-All-Accepted-Orders-Count"), Authorize(Role.Provider)]
        public async Task<ActionResult<int>> GetAllAcceptedOrdersCount()
        {
            var response = await _analyticsRepo.GetTotalAcceptedOrders(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }

        [HttpGet("Get-All-Declined-Orders-Count"), Authorize(Role.Provider)]
        public async Task<ActionResult<int>> GetAllDeclinedOrdersCount()
        {
            var response = await _analyticsRepo.GetTotalDeclinedOrders(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }

        [HttpGet("Get-Doughnut-Data"), Authorize(Role.Provider)]
        public async Task<ActionResult> GetDoughnutChart()
        {
            var response = await _analyticsRepo.GetDoughnutChart(_jwtUtils.GetUserId(HttpContext));
            return Ok();
        }
    }
}
