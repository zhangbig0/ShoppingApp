using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAppApi.Entity;
using ShoppingAppApi.Infrastructure;

namespace ShoppingAppApi.Services
{
    public class GoodsRepository : IGoodsRepository
    {
        private readonly AppDbContext _context;

        public GoodsRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Goods> GetAll()
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

        public Goods AddGoods(Goods goods)
        {
            goods.Id = Guid.NewGuid();

            try
            {
                _context.Goods.Add(goods);
                _context.SaveChanges();
                return goods;
            }
            catch (DbUpdateException e)
            {
                if (_context.Goods.Any(x => x.Id == goods.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        
        public Goods DeleteGoods(Guid guid)
        {
            Goods deleteGoods = _context.Goods.Find(guid);
            if (deleteGoods == null)
            {
                return null;
            }
            else
            {
                _context.Goods.Remove(deleteGoods);
                _context.SaveChanges();
                return deleteGoods;
            }
        }

        public Goods UpdateGoods(Goods goods)
        {
            var entry = _context.ChangeTracker.Entries().FirstOrDefault(entityEntry => entityEntry.Entity == goods);

            _context.Goods.Attach(goods);
            _context.Entry(goods).State = EntityState.Modified;
            _context.SaveChanges();
            return goods;
        }
    }
}