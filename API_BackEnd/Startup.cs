using BLL_BackEnd;
using BLL_BackEnd.Configuration;
using BLL_BackEnd.Models.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;
using API_BackEnd.API;

namespace API_BackEnd
{
    public class Startup
    {
        readonly IWebHostEnvironment HostingEnvironment;

        IConfigurationRoot Configurations { get; }

        public Startup(IWebHostEnvironment env){
            HostingEnvironment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configurations = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services){
            services.AddDbContext<PruebaContext>(builder =>{
                var connStr = Configurations["Data:SqlServerConnectionString"]?? "Data Source=.;Initial Catalog=Prueba_Tecnica;Persist Security Info=True;App=Prueba_Tecnica";
                builder.UseSqlServer(connStr, opt => opt.EnableRetryOnFailure());
            });
            var config = new ApplicationConfiguration();
            Configurations.Bind("Application", config);
            services.AddSingleton(config);
            App.Configuration = config;
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>{
                c.SwaggerDoc("v1", new OpenApiInfo{
                    Version = "v1",
                    Title = "Prueba Técnica .NET Core API v1",
                    Description = "ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact{
                        Name = "Jorge Martinez",
                        Email = "jm21355196@hotmail.com",
                    },
                    License = new OpenApiLicense{
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
            services.AddOptions();
            services.AddSingleton<IConfigurationRoot>(Configurations);
            services.AddSingleton<IConfiguration>(Configurations);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        // required if AllowCredentials is set also
                        .SetIsOriginAllowed(s => true)
                        //.AllowAnyOrigin()
                        .AllowAnyMethod()  // doesn't work for DELETE!
                        .WithMethods("DELETE")
                        .AllowAnyHeader()
                        .AllowCredentials()
                );
            });
            // Instance injection
            services.AddScoped<ClientesRepository>();
            services.AddScoped<MovimientosRepository>();
            services.AddScoped<InventariosRepository>();
            services.AddScoped<CatalogosRepository>();
            // Per request injections
            services.AddScoped<ApiExceptionFilter>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            ILoggerFactory loggerFactory,
            PruebaContext albumContext,
            IConfiguration configuration){
            Log.Logger = new LoggerConfiguration()
                    .WriteTo.RollingFile(pathFormat: "logs\\log-{Date}.log")
                    .CreateLogger();
            loggerFactory.AddSerilog();
            app.UseDeveloperExceptionPage();
            Console.WriteLine("\r\nPlatform: " + System.Runtime.InteropServices.RuntimeInformation.OSDescription);
            string useSqLite = Configurations["Data:useSqLite"];
            Console.WriteLine(useSqLite == "true" ? "SqLite" : "Sql Server");
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePages();
            app.UseDefaultFiles(); 
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba Técnica .NET Core API v1");
            });
        }

    }
}
