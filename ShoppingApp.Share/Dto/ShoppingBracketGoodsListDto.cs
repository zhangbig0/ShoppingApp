using System;
using System.Collections.Generic;

namespace ShoppingApp.Share.Dto
{
    public class ShoppingBracketGoodsListDto
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public List<BracketGoodsDto> GoodsList { get; set; }
        public int Count { get; set; }
    }
}