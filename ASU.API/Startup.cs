﻿using ASU.Core.Constants;
using ASU.Core.DI;
using ASU.Core.Services;
using ASU.Core.Settings;
using ASU.Core.Swagger;
using ASU.Services;
using ASU.Services.Automapper;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;
using ASU.Infrastructure.Middlewares;

namespace ASU.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AuthorizationSettings>(Configuration.GetSection(AppSettings.AuthorizationSettings));

            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddPolicy(AppConstants.CorsPolicy, 
                    builder => builder.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ServiceModelsMap>();
            });

            services.AddSingleton<AutoMapper.IConfigurationProvider>(config);
            services.AddSingleton<IMapper>(sp => new Mapper(config, sp.GetService));

            services.AddTransient<IClaimsService, ClaimsService>();

            var authSettings = Configuration.GetSection(AppSettings.AuthorizationSettings).Get<AuthorizationSettings>();
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authSettings.Issuer,
                        ValidAudience = authSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.SecretKey))
                    };
                });

            services.AddAuthorization();

            services.AddControllers().AddNewtonsoftJson();
            services.AddMvc().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            var drTypes = new[]
            {
                typeof(Database.DependencyRegistrar),
                typeof(Services.DependencyRegistrar),
            };
            foreach (var drType in drTypes)
            {
                var dependencyRegistrar = (IDependencyRegistrar)Activator.CreateInstance(drType);
                dependencyRegistrar?.Register(services, Configuration);
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASU.API", Version = "v1" });
                //c.DocumentFilter<SwaggerEndpointFilter>();
                c.CustomSchemaIds(scheme => scheme.FullName);
                c.OperationFilter<SwaggerAuthorizationFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASU.API"));
            }

            app.UseCors(AppConstants.CorsPolicy);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.ConfigureCustomExceptionMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
