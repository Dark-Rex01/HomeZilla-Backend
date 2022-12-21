using AutoMapper;
using Final.Entities;
using Final.Model.Auth;
using Final.Model.CustomerDashboard;

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

        }
    }
}
