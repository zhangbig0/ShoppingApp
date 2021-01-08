using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Share.Dto
{
    public class OrderDto
    {
        [Key] public Guid Id { get; set; }
        public int Count { get; set; }
        public string GoodsName { get; set; }
        public decimal GoodsPrice { get; set; }
        public string DeliverAddress { get; set; }
        public Decimal TotalPrice { get; set; }
        [DisplayName("账户")] public string Account { get; set; }
    }
}