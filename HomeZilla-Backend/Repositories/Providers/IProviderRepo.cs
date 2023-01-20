using Final.Entities;
using HomeZilla_Backend.Entities;
using HomeZilla_Backend.Models.Customers;
using HomeZilla_Backend.Models.Providers;

namespace HomeZilla_Backend.Repositories.Providers
{
    public interface IProviderRepo
    {
        Task<ProviderUserData?> GetUserData(Guid Id);
        Task<ProviderUserData?> UpdateUserData(ProviderUpdateData Data, Guid Id);
        Task ChangePassword(ChangePassword Data, Guid Id);
        Task<OrderResponse> CurrentOrder(OrderQuery Data, Guid Id);
        Task<OrderResponse> PastOrder(OrderQuery Data, Guid Id);
        Task UpdateProfile(ProfilePic Data, Guid Id);
        Task DeleteProfile(Guid Id);
        Task<List<GetService>> GetService(Guid Id);
        Task AddService(AddService Data, Guid Id);
        Task DeleteService(DeleteService Data, Guid Id);
        Task UpdateService(UpdateService Data, Guid Id);
        Task<AvailableService> CheckService(Guid Id);
        Task<List<Location>> GetLocation();
    }
}
