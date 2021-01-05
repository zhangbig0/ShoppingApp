using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppApi.Entity;

namespace ShoppingAppApi.Services
{
    public interface IGoodsRepository
    {
        public List<Goods> GetAll();
        public IList<Goods> GetByGoodName(string goodsName);
        public Goods GetById(Guid guid);
        public Goods AddGoods(Goods goods);
        public Goods DeleteGoods(Guid guid);
        public Goods UpdateGoods(Goods goods);
    }
}