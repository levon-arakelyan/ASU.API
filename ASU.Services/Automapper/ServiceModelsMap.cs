using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;

namespace ASU.Services.Automapper
{
    public class ServiceModelsMap : AutoMapper.Profile
    {
        public ServiceModelsMap()
        {
            CreateMap<AudienceDTO, Audience>();

            CreateMap<CourseDTO, Course>();

            CreateMap<CourseSubjectDTO, CourseSubject>();

            CreateMap<DepartmentDTO, Department>();
            CreateMap<Department, DepartmentDTO>();

            CreateMap<DepartmentHeadDTO, DepartmentHead>();
            CreateMap<DepartmentHead, DepartmentHeadDTO>();

            CreateMap<FacultyDTO, Faculty>();
            CreateMap<Faculty, FacultyDTO>();

            CreateMap<FacultyHeadDTO, FacultyHead>();

            CreateMap<FacultyHead, FacultyHeadDTO>();

            CreateMap<ProfessionDTO, Profession>();
            CreateMap<Profession, ProfessionDTO>();

            CreateMap<ScheduleDTO, Schedule>();

            CreateMap<StudentDTO, Student>();
            CreateMap<StudentDTO, AuthenticatedUser>()
                .ForMember(d => d.Role, opt => opt.MapFrom(s => UserRole.Student))
                .ForMember(d => d.IsFacultyHead, opt => opt.MapFrom(s => false))
                .ForMember(d => d.IsDepartmentHead, opt => opt.MapFrom(s => false));

            CreateMap<StudentSubjectDTO, StudentSubject>();

            CreateMap<SubjectDTO, Subject>();

            CreateMap<TeacherDTO, Teacher>();
            CreateMap<Teacher, TeacherDTO>();
            CreateMap<TeacherDTO, FacultyHead>()
               .ForMember(d => d.Id, opt => opt.Ignore())
               .ForMember(d => d.FacultyId, opt => opt.MapFrom(s => s.Department.Faculty.Id))
               .ForMember(d => d.TeacherId, opt => opt.MapFrom(s => s.Id));
            CreateMap<TeacherDTO, DepartmentHead>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.DepartmentId, opt => opt.MapFrom(s => s.Department.Id))
                .ForMember(d => d.TeacherId, opt => opt.MapFrom(s => s.Id));
            CreateMap<TeacherDTO, AuthenticatedUser>()
                .ForMember(d => d.Role, opt => opt.MapFrom(s => UserRole.Teacher))
                .ForMember(d => d.IsDepartmentHead, opt => opt.MapFrom(s => s.DepartmentHead != null))
                .ForMember(d => d.IsFacultyHead, opt => opt.MapFrom(s => s.FacultyHead != null));

            CreateMap<TeacherSubjectDTO, TeacherSubject>();
           



        }
    }
}
