using ShoppingApp.Share.Dto;
using ShoppingAppApi.Entity;

namespace ShoppingAppApi.Profile
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .IncludeMembers(x => x.Customer)
                .ForMember(dest => dest.Account,
                    opt => opt.MapFrom(
                        src => src.Customer.Account))
                .ForMember(dest => dest.GoodsName,
                    opt => opt.MapFrom(
                        src => src.Goods.Name))
                .ForMember(dest => dest.GoodsPrice,
                    opt => opt.MapFrom(
                        src => src.Goods.Price))
                .ReverseMap();
            CreateMap<Customer, OrderDto>()
                .ReverseMap();
        }
    }
}