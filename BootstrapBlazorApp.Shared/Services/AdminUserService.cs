using System;
using System.Collections.Generic;
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

        public async Task<List<AdminUserDto>> GetAll()
        {
            return await JsonSerializer.DeserializeAsync<List<AdminUserDto>>(
                await _httpClient.GetStreamAsync("/api/AdminUser/"), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
        }

        public async Task<AdminUserDto> AddAdminUser(AdminUserAddOrUpdateDto adminUserAddOrUpdateDto)
        {
            var serialize = JsonSerializer.Serialize(adminUserAddOrUpdateDto);
            var stringContent = new StringContent(serialize, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/AddAdmin/Add/", stringContent);
            if (response.IsSuccessStatusCode)
            {
                var adminUserDto = await JsonSerializer.DeserializeAsync<AdminUserDto>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
                return adminUserDto;
            }

            return null;
        }

        public async Task<AdminUserDto> UpdateAdminUser(Guid adminUserId,
            AdminUserAddOrUpdateDto adminUserAddOrUpdateDto)
        {
            var serialize = JsonSerializer.Serialize(adminUserAddOrUpdateDto);
            var stringContent = new StringContent(serialize, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/AdminUser/Update/", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<AdminUserDto>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
            }

            return null;
        }

        public async Task<List<AdminUserDto>> DeleteAdminUser(List<Guid> adminUserIds)
        {
            var serialize = JsonSerializer.Serialize(adminUserIds);
            var stringContent = new StringContent(serialize, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/AdminUser/Delete/", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<AdminUserDto>>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
            }

            return null;
        }
    }
}