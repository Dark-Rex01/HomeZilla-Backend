using AutoMapper;
using Final.Data;
using Final.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeZilla_Backend.Repositories.Analytics
{
    public class AnalyticsRepo : IAnalyticsRepo
    {
        private readonly HomezillaContext _context;
        private readonly IMapper _mapper;
        
        public AnalyticsRepo(HomezillaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Customer Analytics
        public async Task<int> GetCustomerTotalOrders(Guid Id)
        {
            var user = await _context.Customer.Where(x => x.CustomerUserID == Id).SingleOrDefaultAsync();
            var response = await _context.OrderDetails.Where(x => x.CustomerId == user.Id)
                                                      .CountAsync();
            return response;
        }
        public async Task<int> GetCustomerTotalAcceptedOrders(Guid Id)
        {
            var user = await _context.Customer.Where(x => x.CustomerUserID == Id).SingleOrDefaultAsync();
            var response = await _context.OrderDetails.Where(x => x.CustomerId == user.Id)
                                                      .GroupBy(s => s.Status == OrderStatus.Accepted)
                                                      .CountAsync();
            return response;
        }

        public async Task<int> GetCustomerTotalCanceledOrders(Guid Id)
        {
            var user = await _context.Customer.Where(x => x.CustomerUserID == Id).SingleOrDefaultAsync();
            var response = await _context.OrderDetails.Where(x => x.CustomerId == user.Id)
                                                      .GroupBy(s => s.Status == OrderStatus.Cancelled)
                                                      .CountAsync();
            return response;
        }
        public async Task<int> GetCustomerTotalWaitingOrders(Guid Id)
        {
            var user = await _context.Customer.Where(x => x.CustomerUserID == Id).SingleOrDefaultAsync();
            var response = await _context.OrderDetails.Where(x => x.CustomerId == user.Id)
                                                      .GroupBy(s => s.Status == OrderStatus.Waiting)
                                                      .CountAsync();
            return response;
        }

        public async Task<List<int>> GetCustomerDoughnutChart(Guid Id)
        {

            var user = await _context.Customer.Where(x => x.CustomerUserID == Id).SingleOrDefaultAsync();
            var data = await _context.OrderDetails.Where(x => x.CustomerId == user.Id).ToListAsync();
            var response = new List<int>();

            foreach (OrderStatus status in Enum.GetValues(typeof(OrderStatus)))
            {
                var count = data.Where(x => x.CustomerId == user.Id).GroupBy(s => s.Status == status).Count();
                response.Add(count);
            }
            return response;
        }
        public async Task<List<int>> GetCustomerLineChart(Guid Id)
        {
            var user = await _context.Customer.Where(x => x.CustomerUserID == Id).SingleOrDefaultAsync();
            var data = _context.OrderDetails.Where(x => x.CustomerId == user.Id && (x.AppointmentFrom >= DateTime.Today.AddMonths(-3) && x.AppointmentFrom <= DateTime.Today)).ToList();
            var response = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                response.Add(_context.OrderDetails.Where(x => x.CustomerId == user.Id && (x.AppointmentFrom.Month == DateTime.Today.AddMonths(-i).Month)).Count());
            }
            return response;
        }

        //Provider Analytics
        public async Task<int> GetProviderTotalOrders(Guid Id)
        {
            var user = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var response = await _context.OrderDetails.Where(x =>x.ProviderId== user.Id)
                                                      .CountAsync();
            return response;
        }

        public async Task<int> GetProviderTotalAcceptedOrders(Guid Id)
        {
            var user = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var response = await _context.OrderDetails.Where(x =>x.ProviderId== user.Id)
                                                      .GroupBy(s =>s.Status == OrderStatus.Accepted)
                                                      .CountAsync();
            return response;
        }

        public async Task<int> GetProviderTotalDeclinedOrders(Guid Id)
        {
            var user = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var response = await _context.OrderDetails.Where(x => x.ProviderId == user.Id)
                                                     .GroupBy(s => s.Status == OrderStatus.Declined)
                                                     .CountAsync();
            return response;
        }


        public async Task<List<int>> GetProviderDoughnutChart(Guid Id)
        {
            
            var user = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var data = await _context.OrderDetails.Where(x => x.ProviderId == user.Id).ToListAsync();
            var response = new List<int>();
            
            foreach (OrderStatus status in Enum.GetValues(typeof(OrderStatus)))
            {
                var count = data.Where(x => x.ProviderId == user.Id).GroupBy(s => s.Status == status).Count();
                response.Add(count);
            }
            return response;
        }

        public async Task<List<int>> GetProviderBarChart(Guid Id)
        {
            var user = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var data = _context.OrderDetails.Where(x => x.ProviderId == user.Id && (x.AppointmentFrom >= DateTime.Today.AddMonths(-3) && x.AppointmentFrom <= DateTime.Today)).ToList();
            var response = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                response.Add(_context.OrderDetails.Where(x => x.ProviderId == user.Id && (x.AppointmentFrom.Month == DateTime.Today.AddMonths(-i).Month )).Count());
            }
            return response;
        }
        

    }
}
