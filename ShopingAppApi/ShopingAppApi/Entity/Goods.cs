
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAppApi.Entity
{
    public class Goods
    {
        public Goods()
        {
            InShoppingBracketGoods = new List<ShoppingBracketGoods>();
            InOrders = new List<Order>();
        }

        [Key] public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Class { get; set; }
        public List<ShoppingBracketGoods> InShoppingBracketGoods { get; set; }
        public List<Order> InOrders { get; set; }
        public string ImgSrc { get; set; }
    }
}