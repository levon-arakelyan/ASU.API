using ASU.Core.DI;
using ASU.Core.Swagger;
using Microsoft.OpenApi.Models;

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
            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            var drTypes = new[]
            {
                typeof(Database.DependencyRegistrar),
            };
            foreach (var drType in drTypes)
            {
                var dependencyRegistrar = (IDependencyRegistrar)Activator.CreateInstance(drType);
                dependencyRegistrar?.Register(services, Configuration);
            }

            services.AddControllers();
            services.AddMvc().AddNewtonsoftJson();
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

            //app.UseCors(AppConstants.CorsPolicy);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.ConfigureCustomExceptionMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
