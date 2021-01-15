using ShoppingApp.Share.Dto;
using ShoppingAppApi.Entity;

namespace ShoppingAppApi.Profile
{
    public class AdminUserProfile:AutoMapper.Profile
    {
        public AdminUserProfile()
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