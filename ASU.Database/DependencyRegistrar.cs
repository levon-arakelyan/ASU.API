using ASU.Core.DI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASU.Database
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ASUContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("ASU")), ServiceLifetime.Scoped);

            //services.AddTransient(typeof(IRepositoryAsync<>), typeof(OrderManagementRepository<>));
        }
    }
}
