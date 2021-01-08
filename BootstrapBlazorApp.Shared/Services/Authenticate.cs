using System.Threading.Tasks;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazorApp.Shared.Services
{
    public class Authenticate:IAuthentication
    {
        private readonly ISessionStorageService _sessionStorageService;
        private readonly NavigationManager _navigationManager;

        [Inject]
        public ISessionStorageService SessionStorageService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Authenticate(ISessionStorageService sessionStorageService, NavigationManager navigationManager)
        {
            _sessionStorageService = sessionStorageService;
            _navigationManager = navigationManager;
        }
        public async Task AuthenticateValidate()
        {
            var role = await _sessionStorageService.GetItemAsync<string>("Role");
            if (string.IsNullOrWhiteSpace(role))
            {
                _navigationManager.NavigateTo($"/login");
            }
        }
    }
}