using AutoMapper;
using ShoppingApp.Share.Dto;
using ShoppingAppApi.Entity;

namespace ShoppingAppApi.Profile
{
    public class CustomerProfile:AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ReverseMap();

        }
    }
}