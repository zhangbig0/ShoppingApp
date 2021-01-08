using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
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
        private readonly IMapper _mapper;

        public ShoppingBracketsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                    return _mapper.Map<ShoppingBracketGoodsListDto>(shoppingBracketByUser);
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

            return _mapper.Map<ShoppingBracketGoodsListDto>(shoppingBracket);
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
        public async Task<ActionResult<ShoppingBracketGoodsListDto>> PostShoppingBracket(ShoppingBracket shoppingBracket)
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

                return _mapper.Map<ShoppingBracketGoodsListDto>(shoppingBracket);
            }

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

            return _mapper.Map<ShoppingBracketGoodsListDto>(shoppingBracket);
        }


        [HttpPost("{userId}/{goodsId}")]
        public async Task<ActionResult<ShoppingBracketGoodsListDto>> MinusGoodsToUserShoppingBracket([FromRoute] Guid userId,
            [FromRoute] Guid goodsId)
        {
            var user = await _context.Customer.FindAsync(userId);
            if (user == null)
            {
                return BadRequest("User not exist");
            }

            var shoppingBracket = await _context.ShoppingBracket.Include(bracket => bracket.GoodsList)
                .FirstOrDefaultAsync(x => x.Customer == user);

            if (shoppingBracket == null || shoppingBracket.Count == 0)
            {
                return BadRequest("购物车为空");
            }

            var goods = _context.Goods.Include(x => x.InShoppingBracketGoods)
                .FirstOrDefault(x => x.Id == goodsId);
            if (goods == null)
            {
                return NotFound("商品不存在");
            }


            if (shoppingBracket.GoodsList.Any() == false)
            {
                return BadRequest("购物车为空");
            }

            var bracketGoods = shoppingBracket.GoodsList.FirstOrDefault(x => x.GoodsId == goods.Id);
            if (bracketGoods != null)
            {
                if (bracketGoods.BracketGoodsNum == 0)
                {
                    return BadRequest("商品数量为0，不能继续删除");
                }

                bracketGoods.BracketGoodsNum--;
            }
            else
            {
                return NotFound("购物车不存在此商品");
            }

            _context.Entry(bracketGoods).State = EntityState.Modified;

            shoppingBracket.Count =
                shoppingBracket.GoodsList.Sum(shoppingBracketGoods => shoppingBracketGoods.BracketGoodsNum);
            await _context.SaveChangesAsync();
            return _mapper.Map<ShoppingBracketGoodsListDto>(shoppingBracket);
        }

        [HttpGet("{userId}/{goodsId}/{num}")]
        public async Task<ActionResult<ShoppingBracketGoodsListDto>> ModifyGoodsToUserShoppingBracket(
            [FromRoute] Guid userId,
            [FromRoute] Guid goodsId, int num)
        {
            var user = await _context.Customer.FindAsync(userId);
            if (user == null)
            {
                return BadRequest("User not exist");
            }

            var shoppingBracket = await _context.ShoppingBracket.Include(bracket => bracket.GoodsList)
                .FirstOrDefaultAsync(x => x.Customer == user);

            if (shoppingBracket == null || shoppingBracket.Count == 0)
            {
                return BadRequest("购物车为空");
            }

            var goods = _context.Goods.Include(x => x.InShoppingBracketGoods)
                .FirstOrDefault(x => x.Id == goodsId);

            if (goods == null)
            {
                return NotFound("商品不存在");
            }


            if (shoppingBracket.GoodsList.Any() == false)
            {
                return BadRequest("购物车为空");
            }

            var bracketGoods = shoppingBracket.GoodsList.FirstOrDefault(x => x.GoodsId == goods.Id);
            bracketGoods.BracketGoodsNum = num;
            bracketGoods.TotalPrice =
                bracketGoods.Goods.Price * bracketGoods.BracketGoodsNum;
            _context.Entry(bracketGoods).State = EntityState.Modified;

            shoppingBracket.Count =
                shoppingBracket.GoodsList.Sum(shoppingBracketGoods => shoppingBracketGoods.BracketGoodsNum);



            await _context.SaveChangesAsync();
            return _mapper.Map<ShoppingBracketGoodsListDto>(shoppingBracket);
        }

        // DELETE: api/ShoppingBrackets/5
        [HttpDelete("{userId}/{goodsId}")]
        public async Task<ActionResult<ShoppingBracketGoodsListDto>> DeleteShoppingBracketGoods(Guid userId, Guid goodsId)
        {
            var shoppingBracket = await _context.ShoppingBracket.Include(bracket => bracket.GoodsList)
                .FirstOrDefaultAsync(x => x.CustomerId == userId);
            if (shoppingBracket == null)
            {
                return NotFound();
            }

            var shoppingBracketGoods = shoppingBracket.GoodsList.FirstOrDefault(x => x.GoodsId == goodsId);

            _context.Remove(shoppingBracketGoods);
            shoppingBracket.Count = shoppingBracket.GoodsList.Sum(goods => goods.BracketGoodsNum);
            await _context.SaveChangesAsync();

            return _mapper.Map<ShoppingBracketGoodsListDto>(shoppingBracket);
        }

        [HttpGet]
        private bool ShoppingBracketExists(Guid id)
        {
            return _context.ShoppingBracket.Any(e => e.Id == id);
        }

        [HttpGet("{userId}/{goodsId}")]
        public async Task<ActionResult<BracketGoodsDto>> ModifyShoppingBracketGoodsChecked(Guid userId, Guid goodsId,
            [FromQuery] bool isChecked)
        {
            var shoppingBracket = await _context.ShoppingBracket.Include(x => x.GoodsList)
                .FirstOrDefaultAsync(x => x.CustomerId == userId);

            var shoppingBracketGoods = shoppingBracket.GoodsList.FirstOrDefault(x => x.GoodsId == goodsId);
            shoppingBracketGoods.Checked = isChecked;
            shoppingBracketGoods.TotalPrice =
                shoppingBracketGoods.Goods.Price * shoppingBracketGoods.BracketGoodsNum;

            shoppingBracket.TotalPrice = shoppingBracket.GoodsList.Sum(x => x.TotalPrice);
            await _context.SaveChangesAsync();
            return _mapper.Map<BracketGoodsDto>(shoppingBracketGoods);
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<ShoppingBracketGoodsListDto>> ModifyShoppingBracketGoodsCheckedGroup(Guid userId,
            [FromBody] IEnumerable<Guid> isChecked)
        {
            var shoppingBracket = await _context.ShoppingBracket
                .Include(x => x.GoodsList)
                .ThenInclude(y => y.Goods)
                .FirstOrDefaultAsync(x => x.CustomerId == userId);

            foreach (var shoppingBracketGoods in shoppingBracket.GoodsList)
            {
                shoppingBracketGoods.Checked = isChecked.Contains(shoppingBracketGoods.GoodsId);
                shoppingBracketGoods.TotalPrice =
                    shoppingBracketGoods.Goods.Price * shoppingBracketGoods.BracketGoodsNum;
                _context.Entry(shoppingBracketGoods).State = EntityState.Modified;
            }

            shoppingBracket.TotalPrice = shoppingBracket.GoodsList.Sum(x => x.TotalPrice);
            await _context.SaveChangesAsync();
            return _mapper.Map<ShoppingBracketGoodsListDto>(shoppingBracket);
        }
    }
}