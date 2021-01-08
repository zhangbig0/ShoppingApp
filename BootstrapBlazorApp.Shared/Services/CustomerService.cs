using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Services
{
    public class CustomerService : ICustomerServices
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CustomerDto>> GetCustomerAsync()
        {
            return await JsonSerializer.DeserializeAsync<List<CustomerDto>>(
                await _httpClient.GetStreamAsync("/api/Customers/GetCustomer"),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
        }

        public async Task<CustomerDto> DeleteCustomerAsync(Guid customerId)
        {
            var response = await _httpClient.DeleteAsync($"/api/customers/DeleteCustomer/{customerId}");
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<CustomerDto>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }

            return null;
        }

        public async Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto)
        {
            var serialize = JsonSerializer.Serialize(customerDto);
            var stringContent = new StringContent(serialize, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Customers/PutCustomer/{customerDto.Id}", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<CustomerDto>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
            }

            return null;
        }

        public async Task<CustomerDto> AddCustomerAsync(CustomerDto customerDto)
        {
            var serialize = JsonSerializer.Serialize(customerDto);
            var stringContent = new StringContent(serialize, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/api/Customers/AddCustomer/{customerDto.Id}", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<CustomerDto>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
            }

            return null;
        }

        public async Task<List<CustomerDto>> DeleteManyCustomerAsync(List<Guid> customerIds)
        {
            var serialize = JsonSerializer.Serialize(customerIds);
            var stringContent = new StringContent(serialize, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Customers/DeleteManyCustomer", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<CustomerDto>>(
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