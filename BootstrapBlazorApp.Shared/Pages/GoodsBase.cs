﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using BootstrapBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Pages
{
    public class GoodsBase : ComponentBase
    {
        [Inject] public IGoodsService GoodsService { get; set; }
        [Inject] public ToastService ToastService { get; set; }
        [Inject] public IAuthentication Authentication { get; set; }

        public string ImgSrc { get; set; }
        public Toast Toast { get; set; }

        public GoodsDto SearchModel { get; set; } = new GoodsDto()
        {
            Name = string.Empty,
            Class = string.Empty,
            Id = Guid.Empty,
            Price = decimal.Zero,
            Stock = 0
        };

        public IList<GoodsDto> GoodsDtos { get; set; } = new List<GoodsDto>();


        protected override async Task OnInitializedAsync()
        {
            GoodsDtos = (IList<GoodsDto>) await GoodsService.GetAll();
            await Authentication.AuthenticateValidate();

            await base.OnInitializedAsync();
        }

        protected Task<GoodsDto> OnAddAsync()
        {
            return Task.FromResult(new GoodsDto
            {
                Id = Guid.Empty
            });
        }

        protected async Task<bool> OnSaveAsync(GoodsDto item)
        {
            var goodsAddDto = new GoodsAddOrUpdateDto
            {
                Class = item.Class,
                Name = item.Name,
                Price = item.Price,
                Stock = item.Stock,
                ImgSrc = ImgSrc
            };
            GoodsDto goodsDto;
            if (item.Id == Guid.Empty || item.Id.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                goodsDto = await GoodsService.Add(goodsAddDto);
                if (goodsDto != null)
                {
                    GoodsDtos.Add(goodsDto);
                    return await Task.FromResult(true);
                }
            }
            else
            {
                goodsDto = await GoodsService.Update(item.Id, goodsAddDto);
                if (goodsDto != null)
                {
                    var old = GoodsDtos.Single(x => x.Id == item.Id);
                    old.Class = goodsDto.Class;
                    old.Name = goodsDto.Name;
                    old.Price = goodsDto.Price;
                    old.Stock = goodsDto.Stock;
                    return await Task.FromResult(true);
                }
            }

            ImgSrc = null;
            return await Task.FromResult(false);
        }


        protected Task<QueryData<GoodsDto>> OnEditQueryAsync(QueryPageOptions options) =>
            OnQueryAsync(options, new List<GoodsDto>(GoodsDtos));

        protected Task<QueryData<GoodsDto>> OnQueryAsync(QueryPageOptions options, IList<GoodsDto> item)
        {
            // if (!string.IsNullOrEmpty(SearchModel.Id.ToString()))
            // {
            //     item = item.Where(
            //         x => x.Id.ToString().Contains(SearchModel.Id.ToString(),
            //             StringComparison.OrdinalIgnoreCase)).ToList();
            // }

            if (!string.IsNullOrEmpty(SearchModel.Name))
            {
                item = item
                    .Where(x => x.Name.Contains(SearchModel.Name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(SearchModel.Class))
            {
                item = item
                    .Where(x => x.Class.Contains(SearchModel.Class, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(options.SearchText))
            {
                // ReSharper disable once ComplexConditionExpression
                item = item.Where(x =>
                    x.Id.ToString().Contains(options.SearchText, StringComparison.OrdinalIgnoreCase) ||
                    x.Name.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase) ||
                    x.Class.Contains(options.SearchText,
                        StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return Task.FromResult(new QueryData<GoodsDto>()
            {
                Items = item,
                IsSearch = !string.IsNullOrEmpty(SearchModel.Name) ||
                           // SearchModel.Id != Guid.Empty ||
                           !string.IsNullOrEmpty(SearchModel.Class)
            });
        }

        public async Task OnFileChange(IEnumerable<UploadFile> uploadFiles)
        {
            var uploadFile = uploadFiles.ToList()[0];
            var imgFileName = uploadFile.File.Name;
            if (uploadFile.File != null)
            {
                var openReadStream = uploadFile.File.OpenReadStream();
                MemoryStream memoryStream = new MemoryStream();
                await openReadStream.CopyToAsync(memoryStream);
                Toast?.SetPlacement(Placement.BottomEnd);
                ToastService?.Show(new ToastOption()
                {
                    Category = ToastCategory.Success,
                    Title = "保存成功",
                    Content = JsonSerializer.Serialize(memoryStream.ToArray())
                });
                if (memoryStream != null)
                {
                    var data = memoryStream.ToArray();


                    var uploadsImgSrc = await GoodsService.UploadsImg(data, imgFileName);
                    if (uploadsImgSrc != null)
                    {
                        Toast?.SetPlacement(Placement.BottomEnd);
                        ToastService?.Show(new ToastOption()
                        {
                            Category = ToastCategory.Success,
                            Title = "上传成功",
                            Content = "保存数据成功，4 秒后自动关闭"
                        });
                        ImgSrc = uploadsImgSrc;
                    }
                }
                else
                {
                    memoryStream = new MemoryStream();
                    await openReadStream.CopyToAsync(memoryStream);
                }
            }
        }

        // protected Task<QueryData<GoodsDto>> OnQueryAsync(QueryPageOptions options)
        // {
        //     if (!string.IsNullOrEmpty(SearchModel.Name))
        //         GoodsDtos = GoodsDtos.Where(item =>
        //             item.Name?.Contains(SearchModel.Name, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
        //     if (!string.IsNullOrEmpty(SearchModel.Class))
        //         GoodsDtos = GoodsDtos.Where(item =>
        //             item.Class?.Contains(SearchModel.Class, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
        //     if (!string.IsNullOrEmpty(options.SearchText))
        //         GoodsDtos = GoodsDtos.Where(item => (item.Name?.Contains(options.SearchText) ?? false)
        //                                             || (item.Class?.Contains(options.SearchText) ?? false)).ToList();
        //     var isSearched = true;
        //
        //     var isFiltered = false;
        //     if (options.Filters.Any())
        //     {
        //         GoodsDtos = GoodsDtos.Where(options.Filters.GetFilterFunc<GoodsDto>()).ToList();
        //
        //         // 通知内部已经过滤数据了
        //         isFiltered = true;
        //     }
        //
        //     var isSorted = false;
        //     if (!string.IsNullOrEmpty(options.SortName))
        //     {
        //         // 外部未进行排序，内部自动进行排序处理
        //         var invoker = SortLambdaCache.GetOrAdd(typeof(GoodsDto), key => GoodsDtos.GetSortLambda().Compile());
        //         GoodsDtos = invoker(GoodsDtos, options.SortName, options.SortOrder).ToList();
        //
        //         // 通知内部已经过滤数据了
        //         isSorted = true;
        //     }
        //
        //     var total = GoodsDtos.Count();
        //     return Task.FromResult(new QueryData<GoodsDto>()
        //     {
        //         IsFiltered = isFiltered,
        //         IsSearch = isSearched,
        //         IsSorted = isSorted,
        //         Items = GoodsDtos,
        //         TotalCount = total
        //     });
        // }

        protected async Task<bool> OnDeleteAsync(IEnumerable<GoodsDto> items)
        {
            foreach (var goodsDto in items)
            {
                var delete = await GoodsService.Delete(goodsDto.Id);

                if (delete == null)
                {
                    return await Task.FromResult(false);
                }

                GoodsDtos.Remove(goodsDto);
            }

            return await Task.FromResult(true);
        }
    }
}