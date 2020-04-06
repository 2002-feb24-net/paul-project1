using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using PaulsUsedGoods.DataAccess.Context;
using PaulsUsedGoods.Domain.Interfaces;
using PaulsUsedGoods.DataAccess.Repositories;

namespace Project1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UsedGoodsDbContext>(p => p.UseSqlServer(Configuration.GetConnectionString("myconn")));
            services.AddControllersWithViews();
            services.AddMvc();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ITopicOptionRepository, TopicOptionRepository>();
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddSingleton<IOrder, PaulsUsedGoods.Domain.Model.Order>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
