using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using BootstrapBlazor.Components;
using BootstrapBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Pages
{
    public class GoodsBase : ComponentBase
    {
        [Inject] public IGoodsService GoodsService { get; set; }

        [Inject] public ToastService ToastService { get; set; }
        [Inject] public MessageService MessageService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISessionStorageService SessionStorageService { get; set; }
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

        // public ConcurrentDictionary<Type, Func<IEnumerable<GoodsDto>, string, SortOrder, IEnumerable<GoodsDto>>>
        //     SortLambdaCache { get; set; } = new();


        protected override async Task OnInitializedAsync()
        {
            GoodsDtos = (IList<GoodsDto>) await GoodsService.GetAll();

            // var role = await SessionStorageService.GetItemAsync<string>("Role");
            // if (string.IsNullOrWhiteSpace(role))
            // {
            //     var currentUri = NavigationManager.BaseUri.Substring("http://localhost:5000/".Length);
            //     if (string.IsNullOrWhiteSpace(currentUri))
            //     {
            //         NavigationManager.NavigateTo($"login/");
            //     }
            // }

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
                Stock = item.Stock
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

            return await Task.FromResult(false);
        }

        public async Task OnFileChange(IEnumerable<UploadFile> uploadFiles)
        {
            var uploadFile = uploadFiles.ToList()[0];
            var imgFileName = uploadFile.FileName;
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
                    Toast?.SetPlacement(Placement.BottomEnd);
                    ToastService?.Show(new ToastOption()
                    {
                        Category = ToastCategory.Success,
                        Title = "保存成功",
                        Content = "保存数据成功，4 秒后自动关闭"
                    });
                    var data = memoryStream.ToArray();

                    var uploadsImgSrc = await GoodsService.UploadsImg(data, imgFileName);
                    if (uploadsImgSrc != null)
                    {
                        Toast?.SetPlacement(Placement.BottomEnd);
                        ToastService?.Show(new ToastOption()
                        {
                            Category = ToastCategory.Success,
                            Title = "保存成功",
                            Content = "保存数据成功，4 秒后自动关闭"
                        });
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