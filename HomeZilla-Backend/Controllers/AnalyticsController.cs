using Final.Authorization;
using Final.Entities;
using HomeZilla_Backend.Repositories.Analytics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeZilla_Backend.Controllers
{
    [Route("api/[controller]")]
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

        //Customer Analytics

        [HttpGet("Get-Customer-All-Orders-Count"), Authorize(Role.Customer)]
        public async Task<ActionResult<int>> GetCustomerAllOrdersCount()
        {
            var response = await _analyticsRepo.GetCustomerTotalOrders(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }

        [HttpGet("Get-Customer-Accepted-Orders-Count"), Authorize(Role.Customer)]
        public async Task<ActionResult<int>> GetCustomerAcceptedOrdersCount()
        {
            var response = await _analyticsRepo.GetCustomerTotalAcceptedOrders(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }

        [HttpGet("Get-Customer-Canceled-Orders-Count"), Authorize(Role.Customer)]
        public async Task<ActionResult<int>> GetCustomerCanceledOrdersCount()
        {
            var response = await _analyticsRepo.GetCustomerTotalCanceledOrders(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }

        [HttpGet("Get-Customer-Waiting-Orders-Count"), Authorize(Role.Customer)]
        public async Task<ActionResult<int>> GetCustomerWaitingOrdersCount()
        {
            var response = await _analyticsRepo.GetCustomerTotalWaitingOrders(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }

        [HttpGet("Get-Customer-Doughnut-Data"), Authorize(Role.Customer)]
        public async Task<ActionResult> GetCustomerDoughnutChart()
        {
            var response = await _analyticsRepo.GetCustomerDoughnutChart(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }

        [HttpGet("Get-Customer-LineChart-Data"), Authorize(Role.Customer)]
        public async Task<ActionResult> GetcustomerLineChart()
        {
            var response = await _analyticsRepo.GetCustomerLineChart(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }

        //Providers Analytics

        [HttpGet("Get-Provider-All-Orders-Count"),Authorize(Role.Provider)]
        public async Task<ActionResult<int>> GetProviderAllOrdersCount()
        {
            var response = await _analyticsRepo.GetProviderTotalOrders(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }
        [HttpGet("Get-Provider-All-Accepted-Orders-Count"), Authorize(Role.Provider)]
        public async Task<ActionResult<int>> GetProviderAllAcceptedOrdersCount()
        {
            var response = await _analyticsRepo.GetProviderTotalAcceptedOrders(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }

        [HttpGet("Get-Provider-All-Expired-Orders-Count"), Authorize(Role.Provider)]
        public async Task<ActionResult<int>> GetProviderAllExpiredOrdersCount()
        {
            var response = await _analyticsRepo.GetProviderTotalExpiredOrders(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }


    
        [HttpGet("Get-Provider-Doughnut-Data"), Authorize(Role.Provider)]
        public async Task<ActionResult> GetProviderDoughnutChart()
        {
            var response = await _analyticsRepo.GetProviderDoughnutChart(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }

        [HttpGet("Get-Provider-BarChart-Data"), Authorize(Role.Provider)]
        public async Task<ActionResult> GetProviderBarChart()
        {
            var response = await _analyticsRepo.GetProviderBarChart(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }
    }
}
