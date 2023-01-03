using AutoMapper;
using Final.Entities;
using Final.Model.Search;
using Final.Repositories.Search;
using HomeZilla_Backend.Models.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchRepo _searchRepo;
        private readonly IMapper _mapper;

        public SearchController(ISearchRepo searchRepo, IMapper mapper)
        {
            _searchRepo = searchRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<SearchResponse>> Search([FromQuery] SearchQuery SearchData)
        {
            var Response = await _searchRepo.SearchByQuery(SearchData);
            return Ok(Response);
        }

        [HttpGet("/Get-Provider")]
        public async Task<ActionResult<ProviderData>> GetProviderDetails([FromQuery] GetProviderById ProviderId)
        {
            var Response = await _searchRepo.GetProvider(ProviderId);
            return Ok(Response);
        }
    }
}
