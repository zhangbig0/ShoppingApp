using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppApi.Entity;

namespace ShoppingAppApi.Services
{
    public interface IGoodsRepository
    {
        public IEnumerable<Goods> GetAll();
        public IList<Goods> GetByGoodName(string goodsName);
        public Goods GetById(Guid guid);
        
    }
}