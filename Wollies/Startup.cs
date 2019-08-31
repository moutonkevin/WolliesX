using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Wollies.Contracts;
using Wollies.Domain.Clients;
using Wollies.Domain.Repositories;
using Wollies.Domain.Services;
using Wollies.Domain.Services.SortingStrategies;

namespace Wollies.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public delegate IProductSortingService ProductSortingServiceFactory(SortingOption option);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton(Configuration);

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductSortingService, ProductSortingService>();

            services.AddScoped<IProductSortingOptionService, LowPriceProductSortingService>();
            services.AddScoped<IProductSortingOptionService, HighPriceProductSortingService>();
            services.AddScoped<IProductSortingOptionService, AscendingNameProductSortingService>();
            services.AddScoped<IProductSortingOptionService, DescendingNameProductSortingService>();
            services.AddScoped<IProductSortingOptionService, RecommendedProductSortingService>();

            services.AddSingleton(serviceProvider =>
            {
                return RestService.For<IProductsApiClient>(Configuration["WolliesRootUrl"]);
            });
            services.AddSingleton(serviceProvider =>
            {
                return RestService.For<IShopperHistoryApiClient>(Configuration["WolliesRootUrl"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
