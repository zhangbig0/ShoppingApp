using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using ShoppingApp.Share.Dto;
using Console = System.Console;

namespace BootstrapBlazorApp.Shared.Services
{
    public class GoodsService : IGoodsService
    {
        private readonly HttpClient _httpClient;

        public GoodsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<GoodsDto>> GetAll()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<GoodsDto>>(
                await _httpClient.GetStreamAsync("/api/goods/index"), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<IEnumerable<GoodsDto>> GetForGoodsName(string goodsName)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<GoodsDto>>(
                await _httpClient.GetStreamAsync($"/api/goods/search/?goodsName={goodsName}"), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<GoodsDto> GetOne(Guid guid)
        {
            return await JsonSerializer.DeserializeAsync<GoodsDto>(
                await _httpClient.GetStreamAsync($"/api/goods/guid/{guid.ToString()}"), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
        }

        public async Task<GoodsDto> Add(GoodsAddOrUpdateDto goods)
        {
            var goodsJson = new StringContent(
                JsonSerializer.Serialize(goods), Encoding.UTF8, "application/json");

            var responseMessage = await _httpClient.PostAsync("/api/goods/addgoods/", goodsJson);

            // if (responseMessage.IsSuccessStatusCode)
            // {
            //     return await JsonSerializer.DeserializeAsync<GoodsDto>
            //         (await responseMessage.Content.ReadAsStreamAsync());
            // }
            // return null;
            var streamReader = new StreamReader(await responseMessage.Content.ReadAsStreamAsync());
            return JsonSerializer.Deserialize<GoodsDto>(await streamReader.ReadToEndAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<GoodsDto> Delete(Guid guid)
        {
            return await JsonSerializer.DeserializeAsync<GoodsDto>(
                await _httpClient.GetStreamAsync($"/api/goods/deleteGoods/{guid.ToString()}"), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
        }

        public async Task<GoodsDto> Update(Guid guid, GoodsAddOrUpdateDto goodsAddOrUpdateDto)
        {
            var httpResponseMessage =await  _httpClient.PostAsync(
                $"/api/goods/UpdateGoods/{guid.ToString()}",
                new StringContent(JsonSerializer.Serialize(
                    goodsAddOrUpdateDto), Encoding.UTF8, "application/json"));

            var streamReader = new StreamReader(await httpResponseMessage.Content.ReadAsStreamAsync());
            var readToEndAsync = await streamReader.ReadToEndAsync();
            // return readToEndAsync;
            return JsonSerializer.Deserialize<GoodsDto>(readToEndAsync, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}