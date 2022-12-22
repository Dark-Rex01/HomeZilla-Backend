using Final.Entities;
using Final.Model.Search;

namespace Final.Repositories.Search
{
    public interface ISearchRepo
    {
        Task<SearchResponse> SearchByQuery(SearchQuery SearchData);
        Task<ProviderData> GetProvider(GetProviderById ProviderId);
    }
}
