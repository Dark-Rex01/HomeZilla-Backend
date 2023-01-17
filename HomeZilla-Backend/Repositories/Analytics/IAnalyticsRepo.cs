using HomeZilla_Backend.Models.Analytics;

namespace HomeZilla_Backend.Repositories.Analytics
{
    public interface IAnalyticsRepo
    {
        Task<int> GetTotalOrders(Guid Id);
        Task<int> GetTotalAcceptedOrders(Guid Id);
        Task<int> GetTotalDeclinedOrders(Guid Id);
    }
}
