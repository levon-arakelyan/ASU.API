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
            services.AddTransient<IAudienciesService, AudienciesService>();
            services.AddTransient<IFacultiesService, FacultiesService>();
            services.AddTransient<IProfessionsService, ProfessionsService>();
            services.AddTransient<ITeachersService, TeachersService>();
            services.AddTransient<ICoursesService, CoursesService>();
            services.AddTransient<IStudentsService, StudentsService>();
            services.AddTransient<ISubjectsService, SubjectsService>();
            services.AddTransient<ISchedulesService, SchedulesService>();
            services.AddTransient<ISubjectGroupsService, SubjectGroupsService>();
            services.AddTransient<IDepartmentsService, DepartmentsService>();
            services.AddTransient<IFacultyHeadsService, FacultyHeadsService>();
            services.AddTransient<IDepartmentHeadsService, DepartmentHeadsService>();
            services.AddTransient<ITeacherSubjectsService, TeacherSubjectsService>();
        }
    }
}
