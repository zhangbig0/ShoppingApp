using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public GoodsController(IGoodsRepository goodsRepository, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _goodsRepository = goodsRepository;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GoodsDto>> Index()
        {
            return _mapper.Map<List<GoodsDto>>(_goodsRepository.GetAll());
        }

        [HttpGet("{goodsName}")]
        public ActionResult<List<GoodsDto>> Search(string goodsName)
        {
            return _mapper.Map<List<GoodsDto>>(_goodsRepository.GetByGoodName(goodsName));
        }

        [HttpGet("{guid}")]
        public GoodsDto Detail([FromRoute] Guid guid)
        {
            var goods = _goodsRepository.GetById(guid);
            return _mapper.Map<GoodsDto>(goods);
        }

        [HttpPost]
        public GoodsDto AddGoods([FromBody] GoodsAddOrUpdateDto goodsDto)
        {
            var goods = _goodsRepository.AddGoods(
                _mapper.Map<Goods>(goodsDto));
            return _mapper.Map<GoodsDto>(goods);
        }

        [HttpPost]
        [Route("{guid}")]
        public ActionResult<GoodsDto> UpdateGoods([FromRoute] string guid,
            [FromBody] GoodsAddOrUpdateDto goodsAddOrUpdateDto)
        {
            var goods = _mapper.Map<GoodsAddOrUpdateDto, Goods>(goodsAddOrUpdateDto);
            goods.Id = Guid.Parse(guid);
            var updateGoods = _goodsRepository.UpdateGoods(goods);
            if (updateGoods == null)
            {
                return NotFound();
            }

            return _mapper.Map<GoodsDto>(goods);
        }

        [HttpGet("{guid}")]
        public ActionResult<GoodsDto> DeleteGoods([FromRoute] string guid)
        {
            var deleteGoods = _goodsRepository.DeleteGoods(Guid.Parse(guid));
            return _mapper.Map<GoodsDto>(deleteGoods);
        }

        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<ActionResult<string>> UploadFile(IFormFile file)
        {
            if (file.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "image");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                await file.CopyToAsync(new FileStream(filePath, FileMode.Create));

                return "/image/" + uniqueFileName;
            }
            else
            {
                return BadRequest("文件不存在");
            }
        }
    }
}