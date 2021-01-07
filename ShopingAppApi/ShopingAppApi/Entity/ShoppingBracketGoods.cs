using System;

namespace ShoppingAppApi.Entity
{
    public class ShoppingBracketGoods
    {
        public Guid GoodsId { get; set; }
        public Guid ShoppingBracketId { get; set; }
        public int BracketGoodsNum { get; set; }
        public Goods Goods { get; set; }
        public ShoppingBracket ShoppingBracket { get; set; }
    }
}