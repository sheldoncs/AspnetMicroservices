using Catalog.API.Config;
using Catalog.API.Configuration;
using Catalog.API.Data;
using Catalog.API.Data.Contract;
using Catalog.API.Data.Database;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;


namespace Catalog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            
            Configuration = configuration;
          
        }

        public IConfiguration Configuration { get; }

       
        private MongoDatabaseConfiguration mongoDatabaseConfiguration;
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<EnvironmentConfig>(Configuration);
            
            ConfigureDbContext(services);
            ConfigureRepositories(services);
            ConfigureProviders(services);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });
            });
        }
        public void ConfigureDbContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<MongoDatabaseConfiguration>>();

            services.AddScoped<IMongoDatabaseConfiguration, MongoDatabaseConfiguration>();
           
            mongoDatabaseConfiguration = new MongoDatabaseConfiguration(Configuration);

           
           
            
            var str = mongoDatabaseConfiguration.ToConnectionString();
            
            
            services.AddScoped<IMongoClient, MongoClient>(sp => new MongoClient(mongoDatabaseConfiguration.ToConnectionString()));
            services.AddScoped<ICatalogContext, CatalogContext>();
        }
        public void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
        }
        public void ConfigureProviders(IServiceCollection services)
        {
            services.AddTransient<IProductProvider, ProductProvider>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
