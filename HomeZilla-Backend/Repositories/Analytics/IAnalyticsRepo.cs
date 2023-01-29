using System.Runtime.CompilerServices;

namespace HomeZilla_Backend.Repositories.Analytics
{
    public interface IAnalyticsRepo
    {

        //Customer Dashboard Analytics
        Task<int> GetCustomerTotalOrders(Guid Id);
        Task<int> GetCustomerTotalAcceptedOrders(Guid Id);
        Task<int> GetCustomerTotalCanceledOrders(Guid Id);
        Task<int> GetCustomerTotalWaitingOrders(Guid Id);
        Task<List<int>> GetCustomerDoughnutChart(Guid Id);
        Task<List<int>> GetCustomerLineChart(Guid Id);

        //Provider Dashboard Analytics
        Task<int> GetProviderTotalOrders(Guid Id);
        Task<int> GetProviderTotalAcceptedOrders(Guid Id);
        Task<int> GetProviderTotalExpiredOrders(Guid Id);
        Task<int> GetProviderTotalDeclinedOrders(Guid Id);
        Task<List<int>> GetProviderDoughnutChart(Guid Id);
        Task<List<int>> GetProviderBarChart(Guid Id);
    }
}
