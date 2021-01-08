using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Services
{
    public interface IAdminUserService
    {
        public Task<AdminUserLoginSuccessDto> Login(AdminUserLoginDto adminUserLoginSuccessDto);
        public Task<AdminUserLoginSuccessDto> Register(RegisterDto registerDto);
        public Task<List<AdminUserDto>> GetAll();
        public Task<AdminUserDto> AddAdminUser(AdminUserAddOrUpdateDto adminUserAddOrUpdateDto);
        public Task<AdminUserDto> UpdateAdminUser(Guid adminUserId, AdminUserAddOrUpdateDto adminUserAddOrUpdateDto);
        public Task<List<AdminUserDto>> DeleteAdminUser(List<Guid> adminUserIds);
    }
}