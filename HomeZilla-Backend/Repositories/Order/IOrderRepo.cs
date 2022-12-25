using Final.Entities;
using Final.Model.Order;

namespace Final.Repositories.Order
{
    public interface IOrderRepo
    {
        Task<string> BookOrder(BookOrder OrderData, Guid Id);
        Task<string> CancelOrder(ChangeStatus Status);
        Task<string> DeclineOrder(ChangeStatus Status);
        Task<string> AcceptOrder(ChangeStatus Status);
    }
}
