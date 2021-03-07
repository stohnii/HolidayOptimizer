using HolidayOptimizer.BAL;
using HolidayOptimizer.Cache;
using HolidayOptimizer.DataLayer;
using HolidayOptimizer.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace HolidayOptimizer
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
            services.AddMemoryCache();

            //services.AddTransient<ICacheProvider<Dictionary<string, string>>, RedisCache>();
            services.AddTransient<ICacheProvider<Dictionary<string, string>>, InMemoryCache>();
            services.AddTransient<IHolidayDataManager<Dictionary<string, string>, Dictionary<string, IEnumerable<HolidayDto>>>, HolidayDataManager>();
            services.AddTransient<IHolidayDataProvider<Dictionary<string, IEnumerable<HolidayDto>>>, NagerHolidayDataProvider>();  
            services.AddTransient<IHolidayOptimizerManager, HolidayOptimizerManager>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
