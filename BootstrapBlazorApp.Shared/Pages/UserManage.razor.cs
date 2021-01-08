using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootstrapBlazorApp.Shared.Services;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class UserManage
    {
        public List<AdminUserDto> AdminUserDtos { get; set; } = new List<AdminUserDto>();
        public IAdminUserService AdminUserService { get; set; }


        private Task<AdminUserDto> OnAddAsync()
        {
            return Task.FromResult(new AdminUserDto
            {
                Id = Guid.Empty
            });
        }

        protected override async Task OnInitializedAsync()
        {
            AdminUserDtos = await AdminUserService.GetAll();
        }

        private async Task<bool> OnSaveAsync(AdminUserDto adminUserDto)
        {
            AdminUserDto adminUser;
            var adminUserAddOrUpdateDto = new AdminUserAddOrUpdateDto
            {
                Account = adminUserDto.Account,
                Password = adminUserDto.Password,
                Role = adminUserDto.Role
            };

            if (adminUserDto.Id != Guid.Empty)
            {
                adminUser = await AdminUserService.UpdateAdminUser(adminUserDto.Id,
                    adminUserAddOrUpdateDto);
            }
            else
            {
                adminUser = await AdminUserService.AddAdminUser(adminUserAddOrUpdateDto);
            }

            return adminUser != null;
        }

        private async Task<bool> OnDeleteAsync(IEnumerable<AdminUserDto> adminUserDtos)
        {
            var deleteAdminUser = await AdminUserService.DeleteAdminUser(adminUserDtos.Select(x => x.Id).ToList());
            return deleteAdminUser != null;
        }
    }
}