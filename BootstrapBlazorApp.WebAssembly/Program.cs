using BootstrapBlazorApp.Shared.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using BootstrapBlazorApp.Shared.Services;

namespace BootstrapBlazorApp.WebAssembly
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            // builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44393/") });

            // 增加 BootstrapBlazor 组件
            builder.Services.AddBootstrapBlazor();

            builder.Services.AddHttpClient<IGoodsService, GoodsService>(client =>
                client.BaseAddress = new Uri("https://localhost:8000/"));

            builder.Services.AddHttpClient<IAdminUserService, AdminUserService>(client =>
                client.BaseAddress = new Uri("https://localhost:8000/"));

            builder.Services.AddHttpClient<ICustomerServices, CustomerService>(client =>
                client.BaseAddress = new Uri("https://localhost:8000/"));

            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.AddBootstrapBlazorTableExcelExport();

            builder.Services.AddBlazoredSessionStorage();

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}