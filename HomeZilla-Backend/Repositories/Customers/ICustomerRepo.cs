using HomeZilla_Backend.Models.Customers;

namespace HomeZilla_Backend.Repositories.Customers
{
    public interface ICustomerRepo
    {
        Task<UserData?> GetUserData(Guid Id);
        Task<UserData?> UpdateUserData(UpdateData Data, Guid Id);
        Task ChangePassword(ChangePassword Data, Guid Id);
        Task<OrderResponse> CurrentOrder(OrderQuery Data, Guid Id);
        Task<OrderResponse> PastOrder(OrderQuery Data, Guid Id);
        Task UpdateProfile(ProfilePic Data, Guid Id);
        Task DeleteProfile(Guid Id);
    }
}
