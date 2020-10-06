using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Planets.ServiceModels;
using Planets.Services;
using Planets.Services.Interfaces;
using System;

namespace Planets
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
            ConfigureHttpClientServices(services);
            ConfigureAutoMapper(services);

            services.AddControllers();
            services.AddSwaggerGen();
        }

        public void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper((mapperConfigurationExpression) =>
            {
                mapperConfigurationExpression.AddProfile(new StarWarsMappingProfile());
            });
        }

        private void ConfigureHttpClientServices(IServiceCollection services)
        {
            services.AddHttpClient<IResidentsService, StarWarsResidentsService>(client =>
            {
                client.BaseAddress = new Uri("https://swapi.dev/");
            });

            services.AddHttpClient<IPlanetsService, StarWarsPlanetsService>(client =>
            {
                client.BaseAddress = new Uri("https://swapi.dev/");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Star Wars API V1");
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
