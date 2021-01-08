using AutoMapper;
using ShoppingApp.Share.Dto;
using ShoppingAppApi.Entity;

namespace ShoppingAppApi.Profile

{
    public class GoodsProfile : AutoMapper.Profile
    {
        public GoodsProfile()
        {
            CreateMap<Goods, GoodsDto>()
                .ForMember(dest => dest.ImgSrc,
                    expression => expression.MapFrom(goods => "https://localhost:8000"+goods.ImgSrc))
                .ReverseMap();
            CreateMap<Goods, GoodsAddOrUpdateDto>()
                .ReverseMap();
            
        }
    }
}