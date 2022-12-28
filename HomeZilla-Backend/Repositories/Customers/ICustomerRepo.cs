using HomeZilla_Backend.Models.Customers;

namespace HomeZilla_Backend.Repositories.Customers
{
    public interface ICustomerRepo
    {
        Task<CustomerUserData?> GetUserData(Guid Id);
        Task<CustomerUserData?> UpdateUserData(CustomerUpdateData Data, Guid Id);
        Task ChangePassword(ChangePassword Data, Guid Id);
        Task<OrderResponse> CurrentOrder(OrderQuery Data, Guid Id);
        Task<OrderResponse> PastOrder(OrderQuery Data, Guid Id);
        Task UpdateProfile(ProfilePic Data, Guid Id);
        Task DeleteProfile(Guid Id);
    }
}
