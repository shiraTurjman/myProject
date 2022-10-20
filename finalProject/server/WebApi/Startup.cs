using Bll.Functions;
using Bll.Interfaces;
using Dal.Functions;
using Dal.Interfaces;
using Dal.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
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
            
            services.AddScoped(typeof(Icategories), typeof(CategoryFunctions));
            services.AddScoped(typeof(ICategoriesBLL), typeof(CategoriesBLL));
            services.AddScoped(typeof(Icolors), typeof(ColorFunctions));
            services.AddScoped(typeof(IColorsBLL), typeof(ColorsFuncBLLcs));
            services.AddScoped(typeof(IEvent), typeof(EventFunctions));
            services.AddScoped(typeof(IEventBLL), typeof(EventFuncBLL));
            services.AddScoped(typeof(IItems), typeof(ItemFunction));
            services.AddScoped(typeof(IItemBLL), typeof(ItemFuncBLL));
            services.AddScoped(typeof(IOutfits), typeof(OutfitsFunction));
            services.AddScoped(typeof(IOutfitsBLL), typeof(OutfitsFuncBLL));
            services.AddScoped(typeof(ITags), typeof(TagFunctions));
            services.AddScoped(typeof(ITagsBLL), typeof(TagsFuncBLL));
            services.AddScoped(typeof(IUser), typeof(UserFunctions));
            services.AddScoped(typeof(IUserBLL), typeof(UserFuncBLL));

            services.AddDbContext<FinalProjectContext>(opt => opt.UseSqlServer("Server = DESKTOP - MS96HH8; Database = FinalProject; Trusted_Connection = true"));

            services.AddCors(opotion => opotion.AddPolicy("AllowAll",//נתינת שם להרשאה
                p => p.AllowAnyOrigin()//מאפשר כל מקור
                .AllowAnyMethod()//כל מתודה - פונקציה
                .AllowAnyHeader()));//וכל כותרת פונקציה


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
