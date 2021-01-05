using System.Text.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using BootstrapBlazor.Components;
using BootstrapBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class Login
    {
        public AdminUserLoginDto LoginDto { get; set; } = new();
        [Inject] public IAdminUserService UserService { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter] public string ReturnUrl { get; set; }
        [Inject] public ISessionStorageService SessionStorageService { get; set; }
        [Inject] public ToastService ToastService { get; set; }
        private Toast Toast { get; set; }


        private async Task OnValidSubmit(EditContext editContext)
        {
            var login = await UserService.Login((AdminUserLoginDto) editContext.Model);
            Toast?.SetPlacement(Placement.BottomEnd);
            ToastService?.Show(new ToastOption()
            {
                Category = ToastCategory.Success,
                Title = "保存成功",
                Content = JsonSerializer.Serialize(login)
            });
            if (login != null)
            {
                await SessionStorageService.SetItemAsync("Role", login.Role);
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