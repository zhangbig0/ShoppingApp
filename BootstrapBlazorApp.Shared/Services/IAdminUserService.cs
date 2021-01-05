using System.Threading.Tasks;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Services
{
    public interface IAdminUserService
    {
        public Task<AdminUserLoginSuccessDto> Login(AdminUserLoginDto adminUserLoginSuccessDto);
        public Task<AdminUserLoginSuccessDto> Register(RegisterDto registerDto);
    }
}