using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class Index
    {
        [Inject] public ISessionStorageService SessionStorageService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            // var role = await SessionStorageService.GetItemAsync<string>("Role");
            // if (string.IsNullOrWhiteSpace(role))
            // {
            //     var currentUri = NavigationManager.BaseUri.Substring("http://localhost:5000/".Length);
            //     NavigationManager.NavigateTo($"login/{currentUri}");
            // }

            await base.OnInitializedAsync();
        }
    }
}