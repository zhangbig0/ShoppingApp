using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppApi.Entity
{
    public class Order
    {
        [Key] public Guid Id { get; set; }
        public Goods Goods { get; set; }
        public Guid GoodsId { get; set; }
        public int Count { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        public string DeliverAddress { get; set; }
        public Decimal TotalPrice { get; set; }
    }
}