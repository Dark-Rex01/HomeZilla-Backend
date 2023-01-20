using AutoMapper;
using Final.Data;
using Final.Entities;
using HomeZilla_Backend.Entities;
using HomeZilla_Backend.Models.Customers;
using HomeZilla_Backend.Models.Providers;
using HomeZilla_Backend.Services.BlobServices;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace HomeZilla_Backend.Repositories.Providers
{
    public class ProviderRepo : IProviderRepo
    {
        private readonly HomezillaContext _context;
        private readonly IMapper _mapper;
        private readonly IBlobService _blobService;

        public ProviderRepo(HomezillaContext context, IMapper mapper, IBlobService blobService)
        {
            _context = context;
            _mapper = mapper;
            _blobService = blobService;
        }

        public async Task<ProviderUserData?> GetUserData(Guid Id)
        {
            var Data = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var Response = _mapper.Map<Provider, ProviderUserData>(Data);
            return Response;
        }

        public async Task<ProviderUserData?> UpdateUserData(ProviderUpdateData Data, Guid Id)
        {
            if (!await _context.Authentication.AnyAsync(x => x.Email == Data.Email && x.AuthId != Id))
            {
                var Query = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
                var AuthData = await _context.Authentication.Where(x => x.AuthId == Id).SingleOrDefaultAsync();
                AuthData.Email = Data.Email;
                AuthData.UserName = Data.UserName;
                _context.Authentication.Update(AuthData);
                _mapper.Map<ProviderUpdateData, Provider>(Data, Query);
                _context.Provider.Update(Query);
                await _context.SaveChangesAsync();
                var Response = _mapper.Map<Provider, ProviderUserData>(Query);
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
            var User = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var OrderData = await _context.OrderDetails.Where(x => x.ProviderId == User.Id &&
                                                       x.Status == OrderStatus.Waiting)
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
            var User = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var OrderData = await _context.OrderDetails.Where(x => x.ProviderId == User.Id &&
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
            var UserData = await _context.Provider.Where(_x => _x.ProviderUserID == Id).SingleOrDefaultAsync();
            if (UserData.ProfilePicture != null)
            {
                await _blobService.Delete(UserData.ProfilePicture);
            }
            var PicUrl = await _blobService.Upload(Data.File);
            UserData.ProfilePicture = PicUrl;
            _context.Provider.Update(UserData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfile(Guid Id)
        {
            var FileName = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            await _blobService.Delete(FileName.ProfilePicture);
            FileName.ProfilePicture = null;
            _context.Provider.Update(FileName);
            await _context.SaveChangesAsync();
        }

        public async Task AddService(AddService Data, Guid Id)
        {
            var UserId = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var ServiceData = _mapper.Map<AddService, ProviderServices>(Data);
            ServiceData.ProviderId = UserId.Id;
            _context.ProviderServices.Add(ServiceData);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GetService>> GetService(Guid Id)
        {
            var UserId = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var ServiceData = await _context.ProviderServices.Where(x => x.ProviderId == UserId.Id).ToListAsync();
            var Response = ServiceData.Select(x => _mapper.Map<ProviderServices, GetService>(x)).ToList();
            return Response;
        }

        public async Task UpdateService(UpdateService Data, Guid Id)
        {
            var UserId = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var ServiceData = await _context.ProviderServices.Where(x => x.ProviderId == UserId.Id
                                                             && x.Id == Data.Id).SingleOrDefaultAsync();
            _mapper.Map<UpdateService, ProviderServices>(Data, ServiceData);
            _context.ProviderServices.Update(ServiceData);
            await _context.SaveChangesAsync();  
        }

        public async Task DeleteService(DeleteService Data, Guid Id)
        {
            var UserId = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();

            var ServiceData = await _context.ProviderServices.Where(x => x.ProviderId == UserId.Id
                                                             && x.Id == Data.Id).SingleOrDefaultAsync();
            _context.ProviderServices.Remove(ServiceData);
            await _context.SaveChangesAsync();
        }

        public async Task<AvailableService> CheckService(Guid Id)
        {
            var UserId = await _context.Provider.Where(x => x.ProviderUserID == Id).SingleOrDefaultAsync();
            var ServiceData = await _context.ProviderServices.Where(x => x.ProviderId == UserId.Id).ToListAsync();
            var AllService = Enum.GetValues(typeof(ServiceList)).Cast<ServiceList>().ToList();
            var res = AllService.Where(p => ServiceData.All(p2 => p2.Service != p )).ToList();
            AvailableService Result = new AvailableService();
            Result.Services = res;
            return Result;
        }

        public List<Location> GetLocation()
        {
            var Response  = Enum.GetValues(typeof(Location)).Cast<Location>().ToList();
            return Response;
        }

    }
}
