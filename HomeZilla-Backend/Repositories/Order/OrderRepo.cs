
using AutoMapper;
using Final.Data;
using Final.Entities;
using Final.Helpers;
using Final.MailServices;
using Final.Model.Order;
using Microsoft.EntityFrameworkCore;

namespace Final.Repositories.Order
{
    public class OrderRepo : IOrderRepo
    {
        private readonly HomezillaContext _context;
        private readonly IMapper _mapper;
        private readonly IMailService _mailer;
        private static MailTemplates mailTemplate = new MailTemplates();
        public OrderRepo(HomezillaContext context, IMapper mapper,
            IMailService mailer)
        {
            _context = context;
            _mapper = mapper;
            _mailer = mailer;
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
                var userEmail = await _context.Customer.Where(x => x.CustomerUserID == Id).Select(x=>x.Email).FirstOrDefaultAsync();
                var Data = new OrderDetails();
                Data = _mapper.Map<BookOrder, OrderDetails>(OrderData);
               
                Data.CustomerId = userId.Id;
                _context.OrderDetails.Add(Data);
                string Template = mailTemplate.OrderConfirmation(Data.ServiceName.ToString(),Data.AppointmentFrom, Data.AppointmentTo);
                await _context.SaveChangesAsync();
                await _mailer.Send(userEmail, "New Order", Template);
                await _mailer.Send(providerEmail.ToString(), "New Order", Template);
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
