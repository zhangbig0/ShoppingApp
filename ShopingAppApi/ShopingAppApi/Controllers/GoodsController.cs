using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using ShoppingApp.Share.Dto;
using ShoppingAppApi.Entity;
using ShoppingAppApi.Services;

namespace ShoppingAppApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodsRepository _goodsRepository;

        public GoodsController(IGoodsRepository goodsRepository)
        {
            _goodsRepository = goodsRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GoodsDto>> Index()
        {
            return _goodsRepository.GetAll().Select(goods => new GoodsDto
            {
                Class = goods.Class,
                Guid = goods.Id,
                Name = goods.Name,
                Price = goods.Price,
                Stock = goods.Stock
            }).ToList();
        }

        [HttpGet]
        public ActionResult<IList<GoodsDto>> Search(string goodsName)
        {
            return _goodsRepository.GetByGoodName(goodsName).Select(goods => new GoodsDto
            {
                Class = goods.Class,
                Guid = goods.Id,
                Name = goods.Name,
                Price = goods.Price,
                Stock = goods.Stock
            }).ToList();
        }

        [HttpGet]
        public GoodsDto Detail([FromRoute] Guid guid)
        {
            var goods = _goodsRepository.GetById(guid);
            return new GoodsDto
            {
                Class = goods.Class,
                Guid = goods.Id,
                Name = goods.Name,
                Price = goods.Price,
                Stock = goods.Stock
            };
        }

        [HttpPost]
        public GoodsDto AddGoods([FromBody] GoodsAddOrUpdateDto goodsDto)
        {
            var goods = _goodsRepository.AddGoods(new Goods
            {
                Class = goodsDto.Class,
                Name = goodsDto.Name,
                Price = goodsDto.Price,
                Stock = goodsDto.Stock
            });
            return new GoodsDto
            {
                Class = goods.Class,
                Guid = goods.Id,
                Name = goods.Name,
                Price = goods.Price,
                Stock = goods.Stock
            };
        }

        [HttpPost]
        [Route("{guid}")]
        public ActionResult<GoodsDto> UpdateGoods([FromRoute] string guid,
            [FromBody] GoodsAddOrUpdateDto goodsAddOrUpdateDto)
        {
            
            var goods = new Goods
            {
                Class = goodsAddOrUpdateDto.Class,
                Name = goodsAddOrUpdateDto.Name,
                Price = goodsAddOrUpdateDto.Price,
                Stock = goodsAddOrUpdateDto.Stock,
                Id = Guid.Parse(guid)
            };
            var updateGoods = _goodsRepository.UpdateGoods(goods);
            if (updateGoods == null)
            {
                return NotFound();
            }

            return new GoodsDto
            {
                Class = updateGoods.Class,
                Guid = updateGoods.Id,
                Name = updateGoods.Name,
                Price = updateGoods.Price,
                Stock = updateGoods.Stock
            };
        }

        [HttpGet("{guid}")]
        public ActionResult<GoodsDto> DeleteGoods([FromRoute] string guid)
        {
            var deleteGoods = _goodsRepository.DeleteGoods(Guid.Parse(guid));
            return new GoodsDto
            {
                Class = deleteGoods.Class,
                Guid = deleteGoods.Id,
                Name = deleteGoods.Name,
                Price = deleteGoods.Price,
                Stock = deleteGoods.Stock
            };
        }
    }
}