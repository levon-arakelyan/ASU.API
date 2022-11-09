using ASU.Core.DI;
using ASU.Core.Services;
using ASU.Core.Services.Utilities;
using ASU.Services.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASU.Services
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthUtility, AuthUtility>();
            services.AddTransient<IAccountsService, AccountsService>();
            services.AddTransient<IFacultiesService, FacultiesService>();
            services.AddTransient<ITeachersService, TeachersService>();
            services.AddTransient<IStudentsService, StudentsService>();
            services.AddTransient<IDepartmentsService, DepartmentsService>();
            services.AddTransient<IFacultyHeadsService, FacultyHeadsService>();
            services.AddTransient<IDepartmentHeadsService, DepartmentHeadsService>();
        }
    }
}
