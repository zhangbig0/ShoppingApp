using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootstrapBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class UserManage
    {
        [Inject] public ICustomerServices CustomerServices { get; set; }

        public List<CustomerDto> CustomerDtos { get; set; } = new List<CustomerDto>();

        public Task<CustomerDto> OnAddAsync()
        {
            return Task.FromResult(new CustomerDto());
        }

        private async Task<bool> OnDeleteAsync(IEnumerable<CustomerDto> customerDtos)
        {
            var customerId = customerDtos.Select(x => x.Id).ToList();
            List<CustomerDto> deletedCustomerDtos = await CustomerServices.DeleteManyCustomerAsync(customerId);

            return deletedCustomerDtos != null;
        }

        protected override async Task OnInitializedAsync()
        {
            CustomerDtos.AddRange(await CustomerServices.GetCustomerAsync());
            await base.OnInitializedAsync();
        }

        private async Task<bool> OnSaveAsync(CustomerDto saveCustomerDto)
        {
            if (saveCustomerDto.Id == Guid.Empty ||
                saveCustomerDto.Id.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                var addCustomer = await CustomerServices.AddCustomerAsync(saveCustomerDto);
                return addCustomer != null;
            }
            else
            {
                var updateCustomerAsync = await CustomerServices.UpdateCustomerAsync(saveCustomerDto);

                return updateCustomerAsync != null;
            }
        }
    }
}