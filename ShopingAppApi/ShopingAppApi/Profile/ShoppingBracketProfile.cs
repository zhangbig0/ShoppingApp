using System.Linq;
using Microsoft.CodeAnalysis;
using ShoppingApp.Share.Dto;
using ShoppingAppApi.Entity;

namespace ShoppingAppApi.Profile
{
    public class ShoppingBracketProfile : AutoMapper.Profile
    {
        public ShoppingBracketProfile()
        {
            CreateMap<ShoppingBracketGoods, BracketGoodsDto>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.GoodsId))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Goods.Name))
                .ForMember(dest => dest.Num,
                    opt => opt.MapFrom(src => src.BracketGoodsNum))
                .ForMember(dest => dest.ImgSrc,
                    opt =>
                        opt.MapFrom(src => "https://localhost:8000" + src.Goods.ImgSrc))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Goods.Price))
                .ForMember(dest => dest.TotalPrice,
                    opt => opt.MapFrom(
                        src => src.BracketGoodsNum * src.Goods.Price))
                .ReverseMap();
            CreateMap<ShoppingBracket, ShoppingBracketGoodsListDto>()
                .ForMember(dest => dest.UserId,
                    option =>
                        option.MapFrom(bracket => bracket.CustomerId))
                .ForMember(dest => dest.GoodsList,
                    options => options.MapFrom(bracket => bracket.GoodsList))
                .ForMember(dest => dest.Count,
                    expression => expression.MapFrom(bracket => bracket.GoodsList.Sum(x => x.BracketGoodsNum)))
                .ForMember(dest => dest.TotalPrice,
                    opt => opt.MapFrom(
                        src => src.GoodsList.Where(x => x.Checked).Sum(x => x.TotalPrice)))
                .ReverseMap();
        }
    }
}