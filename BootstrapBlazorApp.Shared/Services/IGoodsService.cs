using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Services
{
    public interface IGoodsService
    {
        Task<IEnumerable<GoodsDto>> GetAll();
        Task<IEnumerable<GoodsDto>> GetForGoodsName(string goodsName);
        Task<GoodsDto> GetOne(Guid guid);
        Task<GoodsDto> Add(GoodsAddOrUpdateDto goods);
        Task<GoodsDto> Delete(Guid guid);
        Task<GoodsDto> Update(Guid guid, GoodsAddOrUpdateDto goodsAddOrUpdateDto);
        // Task<string> UploadsImg(IBrowserFile browserFile);
        Task<string> UploadsImg(byte[] imgFileBmp, string fileName);
    }
}