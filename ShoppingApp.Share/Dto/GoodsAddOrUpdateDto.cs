using System;
using System.ComponentModel;
using BootstrapBlazor.Components;

namespace ShoppingApp.Share.Dto
{
    public class GoodsAddOrUpdateDto
    {
        [DisplayName("商品名")] public string Name { get; set; }
        [DisplayName("价格")] public decimal Price { get; set; }
        [DisplayName("存货")] public int Stock { get; set; }
        [DisplayName("类别")] public string Class { get; set; }
        [DisplayName("图片链接")] public string ImgSrc { get; set; }
    }
}