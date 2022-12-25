
using AutoMapper;
using Final.Data;
using Final.Entities;
using Final.Model.Order;
using Microsoft.EntityFrameworkCore;

namespace Final.Repositories.Order
{
    public class OrderRepo : IOrderRepo
    {
        private readonly HomezillaContext _context;
        private readonly IMapper _mapper;
        public OrderRepo(HomezillaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> BookOrder(BookOrder OrderData, Guid Id)
        {
            var Check = await _context.OrderDetails
                                .AnyAsync(res => res.AppointmentTo >= OrderData.AppointmentTo && res.AppointmentFrom <= OrderData.AppointmentFrom);
            if(Check)
            {
                return "Provider is Not Available at the given time";
            }
            else
            {
                var userId = await _context.Customer.SingleAsync(x => x.CustomerUserID == Id);
                var Data = new OrderDetails();
                Data = _mapper.Map<BookOrder, OrderDetails>(OrderData);
                Data.CustomerId = userId.Id;
                _context.OrderDetails.Add(Data);
                await _context.SaveChangesAsync();
                return "Placed the Order Successfully";
                
            }
        }
        public async Task<string> CancelOrder(ChangeStatus changeStatus)
        {
            var query = await _context.OrderDetails.Where(x => x.Id == changeStatus.OrderId).FirstAsync();
            query.Status = OrderStatus.Cancelled;
            await _context.SaveChangesAsync();
            return "Order Cancelled Successfully";
        }

        public async Task<string> DeclineOrder(ChangeStatus changeStatus)
        {
            var query = await _context.OrderDetails.Where(x => x.Id == changeStatus.OrderId).FirstAsync();
            query.Status = OrderStatus.Declined;
            await _context.SaveChangesAsync();
            return "Order Declined Successfully";
        }

        public async Task<string> AcceptOrder(ChangeStatus changeStatus)
        {
            var query = await _context.OrderDetails.Where(x => x.Id == changeStatus.OrderId).FirstAsync();
            query.Status = OrderStatus.Accepted;
            await _context.SaveChangesAsync();
            return "Order Accepted Successfully";
        }

    }
}
