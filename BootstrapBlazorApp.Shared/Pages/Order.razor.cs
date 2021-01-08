using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class Order
    {
        public List<OrderDto> OrderDtos { get; set; } = new List<OrderDto>();
        [Inject] public HttpClient HttpClient { get; set; }

        public OrderDto SearchModel { get; set; } = new OrderDto();

        protected override async Task OnInitializedAsync()
        {
            OrderDtos.AddRange(await JsonSerializer.DeserializeAsync<List<OrderDto>>(
                await HttpClient.GetStreamAsync("https://localhost:8000/api/orders/getOrder/"),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                }) ?? new List<OrderDto>());
            await base.OnInitializedAsync();
        }

        protected Task<QueryData<OrderDto>> OnEditQueryAsync(QueryPageOptions options) =>
            BindItemQueryAsync(options, new List<OrderDto>(OrderDtos));

        private Task<QueryData<OrderDto>> BindItemQueryAsync(QueryPageOptions options, List<OrderDto> item)
        {
            if (!string.IsNullOrEmpty(SearchModel.GoodsName))
            {
                item = item.Where(x => x.GoodsName.Contains(SearchModel.GoodsName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(SearchModel.DeliverAddress))
            {
                item = item.Where(x =>
                        x.DeliverAddress.Contains(SearchModel.DeliverAddress, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(SearchModel.Account))
            {
                item = item.Where(x => x.Account.Contains(SearchModel.Account, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(options.SearchText))
            {
                // ReSharper disable once ComplexConditionExpression
                item = item.Where(x => x.GoodsName.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase) ||
                                       // x.DeliverAddress.Contains(options.SearchText,
                                       //     StringComparison.OrdinalIgnoreCase) ||
                                       x.Account.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return Task.FromResult(new QueryData<OrderDto>()
            {
                Items = item,
                IsSearch = !string.IsNullOrEmpty(SearchModel.GoodsName) ||
                           !string.IsNullOrEmpty(SearchModel.DeliverAddress) ||
                           !string.IsNullOrEmpty(SearchModel.Account)
            });
        }
    }
}