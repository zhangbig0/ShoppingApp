using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using ShoppingAppApi.Entity;
using ShoppingAppApi.Services;

namespace ShoppingAppApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodsRepository _goodsRepository;

        public GoodsController(IGoodsRepository goodsRepository)
        {
            _goodsRepository = goodsRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Goods>> Index()
        {
            return _goodsRepository.GetAll().ToList();
        }
        [HttpGet]
        public ActionResult<IList<Goods>> Search(string goodsName)
        {
            return _goodsRepository.GetByGoodName(goodsName).ToList();
        }

        [HttpGet]
        public Goods Detail([FromQuery] Guid guid)
        {
            return _goodsRepository.GetById(guid);
        }
    }
}
