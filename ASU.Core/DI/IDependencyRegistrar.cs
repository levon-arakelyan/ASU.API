using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASU.Core.DI
{
    public interface IDependencyRegistrar
    {
        void Register(IServiceCollection services, IConfiguration configuration);
    }
}
