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

        public int Num { get; set; }
    }
}