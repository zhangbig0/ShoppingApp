using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using BootstrapBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class Index
    {
        [Inject] public IAuthentication Authentication { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Authentication.AuthenticateValidate();
            await base.OnInitializedAsync();
        }
    }
}