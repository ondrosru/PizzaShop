using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PizzaShop.Config;
using PizzaShop.EntityFramework;
using PizzaShop.EntityFramework.Configuration;
using PizzaShop.EntityFramework.Entities;
using PizzaShop.EntityFramework.Repositories;
using PizzaShop.Services;

namespace PizzaShop
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
            ApplicationConfig config = new ApplicationConfig(Configuration);
            services.AddScoped(provider => config.DbContextConfiguration);
            services.AddDbContext<PizzaShopDbContext>((provider, builder) => { builder.UseSqlServer(provider.GetService<DbContextConfiguration>().ConnectionString); });
            services.AddEntityFrameworkSqlServer();

            services.AddScoped<IPizzaRepository, PizzaRepository>();
            services.AddScoped<IPizzaService, PizzaService>();
            services.AddScoped<IRepository<Order>, GenericRepository<Order>>();
            services.AddScoped<IRepository<OrderPrice>, GenericRepository<OrderPrice>>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
