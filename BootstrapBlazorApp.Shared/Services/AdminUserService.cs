using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly HttpClient _httpClient;

        public AdminUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AdminUserLoginSuccessDto> Login(AdminUserLoginDto adminUserLoginDto)
        {
            var content = JsonSerializer.Serialize(adminUserLoginDto);
            HttpContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/AdminUser/Login/", stringContent);

            return await JsonSerializer.DeserializeAsync<AdminUserLoginSuccessDto>(
                await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<AdminUserLoginSuccessDto> Register(RegisterDto registerDto)
        {
            var content = JsonSerializer.Serialize(registerDto);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/AdminUser/Register/", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<AdminUserLoginSuccessDto>
                (await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return null;
        }
    }
}