using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Share.Dto;
using ShoppingAppApi.Entity;
using ShoppingAppApi.Infrastructure;

namespace ShoppingAppApi.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrder()
        {
            var orders = await _context.Order.Include(x => x.Goods).Include(x => x.Customer).ToListAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new {id = order.Id}, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<OrderDto>>> CashFromShoppingBracket([FromRoute] Guid userId)
        {
            var customer = await _context.Customer.FindAsync(userId);

            var shoppingBracket = await _context.ShoppingBracket
                .Include(x => x.GoodsList)
                .ThenInclude(y => y.Goods)
                .FirstOrDefaultAsync(x => x.CustomerId == userId);

            var shoppingBracketGoodsList = shoppingBracket.GoodsList;
            var orders = new List<Order>();
            var shoppingBracketGoodsRemoveList = new List<ShoppingBracketGoods>();

            foreach (var shoppingBracketGoods in shoppingBracketGoodsList)
            {
                if (shoppingBracketGoods.Checked)
                {
                    var order = new Order
                    {
                        Count = shoppingBracketGoods.BracketGoodsNum,
                        Customer = customer,
                        CustomerId = customer.Id,
                        Goods = shoppingBracketGoods.Goods,
                        GoodsId = shoppingBracketGoods.GoodsId,
                        Id = Guid.NewGuid(),
                        TotalPrice = shoppingBracketGoods.Goods.Price * shoppingBracketGoods.BracketGoodsNum
                    };

                    orders.Add(order);
                    _context.Attach(order);
                    await _context.Entry(order).Reference(x => x.Goods).LoadAsync();
                    shoppingBracketGoodsRemoveList.Add(shoppingBracketGoods);
                }
            }

            shoppingBracket.GoodsList.RemoveAll(x => shoppingBracketGoodsRemoveList.Contains(x));
            _context.ShoppingBracketGoods.RemoveRange(shoppingBracketGoodsRemoveList);
            await _context.Order.AddRangeAsync(orders);
            await _context.SaveChangesAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        private bool OrderExists(Guid id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}