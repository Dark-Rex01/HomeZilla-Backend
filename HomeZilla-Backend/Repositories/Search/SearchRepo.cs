using AutoMapper;
using Final.Data;
using Final.Entities;
using Final.Model.Search;
using HomeZilla_Backend.Models.Search;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Final.Repositories.Search
{
    public class SearchRepo : ISearchRepo 
    {
        private readonly IMapper _mapper;
        private readonly HomezillaContext _context;
        public SearchRepo (IMapper mapper, HomezillaContext context)
        {
            _mapper = mapper;
            _context = context; 
        }

        public async Task<SearchResponse> SearchByQuery(SearchQuery SearchData)
        {
            var List = new List<ProviderServices?>();
            var QueryList = await _context.ProviderServices
                                          .ToListAsync();
            List = QueryList.Where(x => x.Service.ToString()
                                              .StartsWith(SearchData.Service, StringComparison.InvariantCultureIgnoreCase))
                                              .GroupBy(x => x.ProviderId).Select(x => x.FirstOrDefault())
                                              .ToList();
            var Ids = new List<Guid>();
            Ids = List.Select(x => x.ProviderId).ToList();
            var searchResult = new List<Provider>();
            searchResult = await _context.Provider.Where(x => Ids.Contains(x.Id) && x.Location.StartsWith(SearchData.Location))
                                                  .ToListAsync();
            int count = searchResult.Count();
            searchResult = searchResult.Skip((SearchData.PageNumber - 1) * 6)
                                       .Take(6)
                                       .ToList();
            var Response = new SearchResponse();
            Response.Data = searchResult.Select(x => _mapper.Map<Provider, ProviderList>(x));
            Response.CurrentPage = SearchData.PageNumber;
            Response.TotalPages = count / 6;
            return Response;
        }

        public async Task<ProviderData> GetProvider(GetProviderById ProviderId)
        {
            var Data = await _context.Provider.FirstAsync(x => x.Id == ProviderId.Id);
            var query = await _context.ProviderServices.Where(c => c.ProviderId == ProviderId.Id)
                                                               .ToListAsync();
            var ProviderData = new ProviderData();
            ProviderData = _mapper.Map<ProviderData>(Data);
            var serviceList = new List<ServiceData>();
            serviceList = _mapper.Map<List<ServiceData>>(query);
            foreach (var data in serviceList)
            {
                ProviderData.ServiceData.Add(data);
            }
            
            return ProviderData;
        }
    }
}
