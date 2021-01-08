using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppApi.Entity
{
    public class ShoppingBracket
    {
        public ShoppingBracket()
        {
            GoodsList = new List<ShoppingBracketGoods>();
        }

        [Key] public Guid Id { get; set; }
        public List<ShoppingBracketGoods> GoodsList { get; set; }
        public int Count { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}