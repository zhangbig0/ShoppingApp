using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Services
{
    public interface ICustomerServices
    {
        public Task<List<CustomerDto>> GetCustomerAsync();
        public Task<CustomerDto> DeleteCustomerAsync(Guid customerId);
        public Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto);
        public Task<CustomerDto> AddCustomerAsync(CustomerDto customerDto);
        public Task<List<CustomerDto>> DeleteManyCustomerAsync(List<Guid> customerIds);
    }
}