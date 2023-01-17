using AutoMapper;
using Final.Data;
using Final.Entities;
using HomeZilla_Backend.Models.Analytics;
using Microsoft.EntityFrameworkCore;
using Realms.Sync;

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

        public async Task<int> GetTotalOrders(Guid Id)
        {
            var user = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var response = await _context.OrderDetails.Where(x =>x.ProviderId== user.Id)
                                                      .CountAsync();
            return response;
        }

        public async Task<int> GetTotalAcceptedOrders(Guid Id)
        {
            var user = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var response = await _context.OrderDetails.Where(x =>x.ProviderId== user.Id)
                                                      .GroupBy(s =>s.Status == OrderStatus.Accepted)
                                                      .CountAsync();
            return response;
        }

        public async Task<int> GetTotalDeclinedOrders(Guid Id)
        {
            var user = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var response = await _context.OrderDetails.Where(x => x.ProviderId == user.Id)
                                                     .GroupBy(s => s.Status == OrderStatus.Declined)
                                                     .CountAsync();
            return response;
        }

    }
}
