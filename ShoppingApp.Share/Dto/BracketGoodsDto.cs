using System;
using System.ComponentModel;
using BootstrapBlazor.Components;

namespace ShoppingApp.Share.Dto
{
    public class BracketGoodsDto
    {
        [AutoGenerateColumn(Visible = false)] public Guid Id { get; set; }

        [DisplayName("商品名")]
        [AutoGenerateColumn(Sortable = true)]
        public string Name { get; set; }

        public Decimal Price { get; set; }
        public string ImgSrc { get; set; }

        public int Num { get; set; }
        public bool Checked { get; set; }
        public decimal TotalPrice { get; set; }
    }
}