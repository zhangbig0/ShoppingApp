using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Share.Dto;
using ShoppingAppApi.Entity;
using ShoppingAppApi.Infrastructure;

namespace ShoppingAppApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShoppingBracketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShoppingBracketsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingBrackets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingBracket>>> GetShoppingBracket()
        {
            return await _context.ShoppingBracket.ToListAsync();
        }

        // GET: api/ShoppingBrackets/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<ShoppingBracketGoodsListDto>> GetShoppingBracketByUser([FromRoute] Guid userId)
        {
            var user = await _context.Customer.FindAsync(userId);
            if (user == null)
            {
                return BadRequest("User not existence");
            }
            var shoppingBracket = await _context.ShoppingBracket.Include(x => x.GoodsList)
                .ThenInclude(x => x.Goods)
                .FirstOrDefaultAsync(x => x.Customer.Id == user.Id);

            if (shoppingBracket == null)
            {
                var shoppingBracketByUser = new ShoppingBracket
                {
                    Count = 0,
                    Customer = user,
                    GoodsList = new List<ShoppingBracketGoods>(),
                    Id = Guid.NewGuid()
                };

                var entity = await _context.ShoppingBracket.AddAsync(shoppingBracketByUser);
                await _context.SaveChangesAsync();

                if (entity != null)
                {
                    return new ShoppingBracketGoodsListDto()
                    {
                        Count = 0,
                        UserId = user.Id,
                        GoodsList = new List<BracketGoodsDto>(),
                        Id = shoppingBracketByUser.Id,
                    };
                }

                return BadRequest("创建购物车失败");
            }
            shoppingBracket.Count = shoppingBracket.GoodsList.Sum(x => x.BracketGoodsNum);
            await _context.SaveChangesAsync();
            if (shoppingBracket.GoodsList == null)
            {
                shoppingBracket.GoodsList = new List<ShoppingBracketGoods>();
                await _context.SaveChangesAsync();
            }
            return new ShoppingBracketGoodsListDto
            {
                Count = shoppingBracket.Count,
                GoodsList = shoppingBracket.GoodsList.Select(x => new BracketGoodsDto
                {
                    Id = x.GoodsId,
                    Name = x.Goods.Name,
                    Num = x.BracketGoodsNum,
                }).ToList(),
                Id = shoppingBracket.Id,
                UserId = shoppingBracket.CustomerId,
            };
        }

        // PUT: api/ShoppingBrackets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingBracket(Guid id, ShoppingBracket shoppingBracket)
        {
            if (id != shoppingBracket.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingBracket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingBracketExists(id))
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

        // POST: api/ShoppingBrackets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingBracket>> PostShoppingBracket(ShoppingBracket shoppingBracket)
        {
            await _context.ShoppingBracket.AddAsync(shoppingBracket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingBracket", new {id = shoppingBracket.Id}, shoppingBracket);
        }

        [HttpPost("{userId}/{goodsId}")]
        public async Task<ActionResult<ShoppingBracketGoodsListDto>> AddGoodsToUserShoppingBracket(
            [FromRoute] Guid userId,
            [FromRoute] Guid goodsId)
        {
            var user = await _context.Customer.FindAsync(userId);
            var shoppingBracket = await _context.ShoppingBracket.Include(x => x.GoodsList)
                .FirstOrDefaultAsync(x => x.Customer == user);

            var goods = _context.Goods.Include(x => x.InShoppingBracketGoods)
                .FirstOrDefault(x => x.Id == goodsId);

            if (shoppingBracket == null)
            {
                var shoppingBracketGoodsListDto = ShoppingBracketGoodsListDtoFromNull(userId, goods);


                var shoppingBracketGoods = new ShoppingBracketGoods()
                {
                    BracketGoodsNum = 1,
                    Goods = goods,
                    GoodsId = goods.Id,
                };

                shoppingBracket = new ShoppingBracket
                {
                    Count = 1,
                    Customer = user,
                    CustomerId = user.Id,
                    GoodsList = new List<ShoppingBracketGoods>()
                    {
                        shoppingBracketGoods,
                    },
                    Id = Guid.NewGuid(),
                };

                await _context.ShoppingBracketGoods.AddAsync(shoppingBracketGoods);
                await _context.ShoppingBracket.AddAsync(shoppingBracket);
                await _context.SaveChangesAsync();

                return shoppingBracketGoodsListDto;
            }

            shoppingBracket.GoodsList ??= new List<ShoppingBracketGoods>();

            if (shoppingBracket.GoodsList.Any(x => x.GoodsId == goods.Id))
            {
                var shoppingBracketGoods = shoppingBracket.GoodsList.First(x => x.GoodsId == goods.Id);
                shoppingBracketGoods.BracketGoodsNum += 1;

                shoppingBracket.Count = shoppingBracket.GoodsList.Sum(x => x.BracketGoodsNum);
                await _context.SaveChangesAsync();
            }
            else
            {
                var item = new ShoppingBracketGoods
                {
                    BracketGoodsNum = 1,
                    Goods = goods,
                    GoodsId = goods.Id,
                    ShoppingBracket = shoppingBracket,
                    ShoppingBracketId = shoppingBracket.Id
                };

                shoppingBracket.GoodsList.Add(item);
                shoppingBracket.Count = shoppingBracket.GoodsList.Sum(x => x.BracketGoodsNum);

                await _context.ShoppingBracketGoods.AddAsync(item);
                await _context.SaveChangesAsync();
            }

            return new ShoppingBracketGoodsListDto
            {
                Count = shoppingBracket.Count,
                GoodsList = shoppingBracket.GoodsList.Select(x => new BracketGoodsDto
                {
                    Id = x.GoodsId,
                    Name = x.Goods.Name,
                    Num = x.BracketGoodsNum
                }).ToList(),
                Id = shoppingBracket.Id,
                UserId = userId
            };
        }

        private static ShoppingBracketGoodsListDto ShoppingBracketGoodsListDtoFromNull(Guid userGuid, Goods goods)
        {
            return new ShoppingBracketGoodsListDto()
            {
                Count = 1,
                Id = Guid.NewGuid(),
                GoodsList = new List<BracketGoodsDto>()
                {
                    new BracketGoodsDto()
                    {
                        Id = goods.Id,
                        Name = goods.Name,
                        Num = 1,
                    }
                },
                UserId = userGuid,
            };
        }


        [HttpPost("{userId}/{goodsId}")]
        public async Task<ActionResult<ShoppingBracket>> DeleteGoodsToUserShoppingBracket([FromRoute] Guid userId,
            [FromRoute] Guid goodsId)
        {
            var user = await _context.Customer.FindAsync(userId);
            if (user==null)
            {
                return BadRequest("User not exist");
            }
            var shoppingBracket = await _context.ShoppingBracket.FirstOrDefaultAsync(x => x.Customer == user);
            if (shoppingBracket==null || shoppingBracket.Count ==0)
            {
                return BadRequest("购物车为空");
            }
            var goods = _context.Goods.Include(x => x.InShoppingBracketGoods)
                .FirstOrDefault(x => x.Id == goodsId);


            if (shoppingBracket.GoodsList.Any() == false)
            {
                return BadRequest("购物车为空");
            }

            if (shoppingBracket.GoodsList.Any(x => x.Goods.Id == goods.Id))
            {

            }

            _context.Entry(shoppingBracket).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return shoppingBracket;
        }

        // DELETE: api/ShoppingBrackets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingBracket(Guid id)
        {
            var shoppingBracket = await _context.ShoppingBracket.FindAsync(id);
            if (shoppingBracket == null)
            {
                return NotFound();
            }

            _context.ShoppingBracket.Remove(shoppingBracket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        private bool ShoppingBracketExists(Guid id)
        {
            return _context.ShoppingBracket.Any(e => e.Id == id);
        }
    }
}