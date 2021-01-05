using System.Threading.Tasks;
using Blazored.SessionStorage;
using BootstrapBlazor.Components;
using BootstrapBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class Register
    {
        public RegisterDto RegisterDto { get; set; } = new();
        [Inject] public ToastService ToastService { get; set; }
        private Toast Toast { get; set; }
        [Inject] public IAdminUserService UserService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter] public string ReturnUrl { get; set; }
        [Inject] public ISessionStorageService SessionStorageService { get; set; }

        private async Task OnValidSubmit(EditContext editContext)
        {
            var editContextModel = (RegisterDto) editContext.Model;
            if (editContextModel.Password == editContextModel.PasswordTwice)
            {
                Toast?.SetPlacement(Placement.BottomEnd);
                ToastService?.Show(new ToastOption()
                {
                    Category = ToastCategory.Error,
                    Title = "保存失败",
                    Content = "两次密码输入相同，4 秒后自动关闭"
                });
                return;
            }

            var register = await UserService.Register(editContextModel);
            if (register != null)
            {
                Toast?.SetPlacement(Placement.BottomEnd);
                ToastService?.Show(new ToastOption()
                {
                    Category = ToastCategory.Success,
                    Title = "注册成功",
                    Content = "注册成功，4 秒后自动关闭"
                });
                await SessionStorageService.SetItemAsync("Role", register.Role);
                NavigationManager.NavigateTo(ReturnUrl);
            }
        }

        protected override void OnParametersSet()
        {
            ReturnUrl ??= "";
            base.OnParametersSet();
        }
    }
}