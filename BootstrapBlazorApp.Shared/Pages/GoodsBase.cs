using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BootstrapBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ShoppingApp.Share.Dto;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class GoodsBase
    {
        [Inject] public IGoodsService GoodsService { get; set; }
        public GoodsDto GoodsDtoInsert { get; set; } = new GoodsDto();
        public IEnumerable<GoodsDto> GoodsDtos { get; set; } = new List<GoodsDto>();


        protected override async Task OnInitializedAsync()
        {
            GoodsDtos = await GoodsService.GetAll();
            await base.OnInitializedAsync();
        }

        public Task OnSubmit(EditContext arg)
        {
            var argModel = (GoodsDto)arg.Model;
            return ;
        }
    }
}