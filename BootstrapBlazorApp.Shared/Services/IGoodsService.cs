using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Services
{
    public interface IGoodsServices
    {
        Task<IEnumerable<GoodsDto>> GetAll();
        Task<IEnumerable<GoodsDto>> GetForGoodsName(string goodsName);
        Task<GoodsDto> GetOne(Guid guid);
        Task<GoodsDto> Add(GoodsAddOrUpdateDto goods);
        Task<GoodsDto> Delete(Guid guid);
    }
}