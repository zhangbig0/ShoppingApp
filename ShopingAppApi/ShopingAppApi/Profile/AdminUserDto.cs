using ShoppingApp.Share.Dto;
using ShoppingAppApi.Entity;

namespace ShoppingAppApi.Profile
{
    public class AdminUserDto:AutoMapper.Profile
    {
        public AdminUserDto()
        {
            CreateMap<AdminUser, AdminUserDto>()
                .ReverseMap();
            CreateMap<AdminUser, AdminUserLoginDto>()
                .ReverseMap();
            CreateMap<AdminUser, AdminUserLoginSuccessDto>()
                .ReverseMap();
            CreateMap<AdminUser, AdminUserAddOrUpdateDto>()
                .ReverseMap();
        }
    }
}