using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppApi.Entity;
using ShoppingAppApi.Infrastructure;

namespace ShoppingAppApi.Services
{
    public class GoodsRepository:IGoodsRepository
    {
        private readonly AppDbContext _context;

        public GoodsRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Goods> GetAll()
        {
            return _context.Goods.ToList();
        }

        public IList<Goods> GetByGoodName(string goodsName)
        {
            return _context.Goods.Where(goods => goods.Name.Contains(goodsName)).ToList();
        }

        public Goods GetById(Guid guid)
        {
            return _context.Goods.Find(guid);
        }
    }
}