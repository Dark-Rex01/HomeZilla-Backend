using AutoMapper;
using Final.Entities;
using Final.Model.Auth;
using Final.Model.CustomerDashboard;
using Final.Model.Order;
using Final.Model.Search;
using HomeZilla_Backend.Models.Customers;
using HomeZilla_Backend.Models.Search;

namespace Final.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Register User
            CreateMap<RegisterRequest, Authentication>()
                .ForMember(o => o.UserRole, ex => ex.MapFrom(o => Enum.Parse(typeof(Role), o.UserRole)));
            CreateMap<RegisterRequest, Customer>();
            CreateMap<RegisterRequest, Provider>();


            CreateMap<Customer, CustomerDetailsRequest>();

            CreateMap<CustomerDetailsRequest, Customer>();

            CreateMap<OrderDetails, OrderDetailsResponse>();

            // Search
            CreateMap<Provider, ProviderList>();
            CreateMap<Provider, ProviderData>();
            CreateMap<ProviderServices, ServiceData>();

            // Order 
            CreateMap<BookOrder, OrderDetails>();

            // Customer 
            CreateMap<Customer, UserData>();
            CreateMap<UpdateData, Customer>();
            CreateMap<OrderDetails, OrderData>();

        }
    }
}
