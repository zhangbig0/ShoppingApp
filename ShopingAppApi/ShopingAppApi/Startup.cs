using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShoppingAppApi.Infrastructure;
using ShoppingAppApi.Services;

namespace ShoppingAppApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContextPool<AppDbContext>(builder =>
            {
                builder.UseMySql("server=121.5.26.37;user=zhangbig;password=zq19990821;database=ShoppingApp",
                    ServerVersion.AutoDetect(
                        "server=121.5.26.37;user=zhangbig;password=zq19990821;database=ShoppingApp"));
            });
            services.AddScoped<IAdminUserRepository, AdminUserRepository>();
            services.AddScoped<IGoodsRepository, GoodsRepository>();
            services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "DataAnalyse.Net", Version = "v1"});
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DataAnalyse.Net v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors("Open");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}