using AutoMapper;
using Final.Data;
using Final.Entities;
using Final.Model.Search;
using HomeZilla_Backend.Models.Customers;
using HomeZilla_Backend.Models.Search;
using HomeZilla_Backend.Services.BlobServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Services;
using System.Security.Authentication;

namespace HomeZilla_Backend.Repositories.Customers
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly HomezillaContext _context;
        private readonly IMapper _mapper;
        private readonly IBlobService _blobService;

        public CustomerRepo(HomezillaContext context, IMapper mapper, IBlobService blobService)
        {
            _context = context;
            _mapper = mapper;
            _blobService = blobService;
        }

        public async Task<CustomerUserData?> GetUserData(Guid Id)
        {
            var Data = await _context.Customer.Where(x => x.CustomerUserID == Id).SingleOrDefaultAsync();
            var Response = _mapper.Map<Customer?, CustomerUserData>(Data);
            return Response;
        }

        public async Task<CustomerUserData?> UpdateUserData(CustomerUpdateData Data, Guid Id)
        {
            if(!await _context.Authentication.AnyAsync(x => x.Email == Data.Email && x.AuthId != Id))
            {
                var Query = await _context.Customer.Where(x => x.CustomerUserID == Id).SingleOrDefaultAsync();
                var AuthData = await _context.Authentication.Where(x => x.AuthId == Id).SingleOrDefaultAsync();
                AuthData.Email = Data.Email;
                AuthData.UserName = Data.UserName;
                _context.Authentication.Update(AuthData);
                _mapper.Map<CustomerUpdateData, Customer>(Data, Query);
                _context.Customer.Update(Query);
                await _context.SaveChangesAsync();
                var Response = _mapper.Map<Customer, CustomerUserData>(Query);
                return Response;
            }
            else
            {
                throw new KeyNotFoundException("Email already registered");
            }
            
        }

        public async Task ChangePassword(ChangePassword Data, Guid Id)
        {
            var Query = await _context.Authentication.Where(x => x.AuthId == Id).SingleOrDefaultAsync();
            if (!BCrypt.Net.BCrypt.Verify(Data.OldPassword, Query.PasswordHash))
            {
                throw new AuthenticationException("Password is Incorrect");
            }
            else
            {
                Query.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Data.NewPassword);
                _context.Authentication.Update(Query);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<OrderResponse> CurrentOrder(OrderQuery Data, Guid Id)
        {
            var User = await _context.Customer.Where(x => x.CustomerUserID == Id).SingleOrDefaultAsync();
            var OrderData = await _context.OrderDetails.Where(x => x.CustomerId == User.Id &&
                                                       x.Status == OrderStatus.Waiting )
                                                       .ToListAsync();
            int count = OrderData.Count();
            OrderData = OrderData.Where(x => x.ServiceName.ToString().StartsWith(Data.ServiceName, StringComparison.InvariantCultureIgnoreCase))
                                 .Skip((Data.PageNumber - 1) * 10)
                                 .Take(10)
                                 .ToList();
            var Response = new OrderResponse();
            Response.Data = OrderData.Select(x => _mapper.Map<OrderDetails, OrderData>(x)).ToList();
            Response.CurrentPage = Data.PageNumber;
            Response.TotalPages = (int)Math.Ceiling((double)count / 10);
            return Response;
        }


        public async Task<OrderResponse> PastOrder(OrderQuery Data, Guid Id)
        {
            var User = await _context.Customer.Where(x => x.CustomerUserID == Id).SingleOrDefaultAsync();
            var OrderData = await _context.OrderDetails.Where(x => x.CustomerId == User.Id &&
                                                       (x.Status == OrderStatus.Accepted ||
                                                       x.Status == OrderStatus.Cancelled ||
                                                       x.Status == OrderStatus.Declined))
                                                       .ToListAsync();
            int count = OrderData.Count();
            OrderData = OrderData.Where(x => x.ServiceName.ToString().StartsWith(Data.ServiceName, StringComparison.InvariantCultureIgnoreCase))
                                 .Skip((Data.PageNumber - 1) * 10)
                                 .Take(10)
                                 .ToList();
            var Response = new OrderResponse();
            Response.Data = OrderData.Select(x => _mapper.Map<OrderDetails, OrderData>(x)).ToList();
            Response.CurrentPage = Data.PageNumber;
            Response.TotalPages = (int)Math.Ceiling((double)count / 10);
            return Response;
        }

        public async Task UpdateProfile(ProfilePic Data, Guid Id)
        {
            var UserData = await _context.Customer.Where(_x => _x.CustomerUserID == Id).SingleOrDefaultAsync();
            if(UserData.ProfilePicture != null)
            {
                await _blobService.Delete(UserData.ProfilePicture);
            }
            var PicUrl = await _blobService.Upload(Data.File);
            UserData.ProfilePicture = PicUrl;
            _context.Customer.Update(UserData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfile(Guid Id)
        {
            var FileName = await _context.Customer.Where(x => x.CustomerUserID == Id).SingleOrDefaultAsync();
            await _blobService.Delete(FileName.ProfilePicture);
            FileName.ProfilePicture = null;
            _context.Customer.Update(FileName);
            await _context.SaveChangesAsync();
        }
    }
}
