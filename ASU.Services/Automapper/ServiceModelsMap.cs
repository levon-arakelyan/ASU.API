﻿using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace ASU.Services.Automapper
{
    public class ServiceModelsMap : AutoMapper.Profile
    {
        public ServiceModelsMap()
        {
            CreateMap<AudienceDTO, Audience>();
            CreateMap<Audience, AudienceDTO>();
            CreateMap<NewAudience, Audience>();


            CreateMap<CourseDTO, Course>();
            CreateMap<NewCourse, Course>();
            CreateMap<Course, CourseDTO>();
            CreateMap<JsonPatchDocument<CourseShortInfo>, JsonPatchDocument<Course>>();
            CreateMap<Operation<CourseShortInfo>, Operation<Course>>();

            CreateMap<CourseSubjectDTO, CourseSubject>();
            CreateMap<CourseSubject, CourseSubjectDTO>();
            CreateMap<JsonPatchDocument<ICollection<CourseSubjectDTO>>, JsonPatchDocument<ICollection<CourseSubject>>>();
            CreateMap<Operation<ICollection<CourseSubjectDTO>>, Operation<ICollection<CourseSubject>>>();

            CreateMap<DepartmentDTO, Department>();
            CreateMap<NewDepartment, Department>();
            CreateMap<Department, DepartmentDTO>();

            CreateMap<DepartmentHeadDTO, DepartmentHead>();
            CreateMap<DepartmentHead, DepartmentHeadDTO>();

            CreateMap<FacultyDTO, Faculty>();
            CreateMap<NewFaculty, Faculty>();
            CreateMap<Faculty, FacultyDTO>();

            CreateMap<FacultyHeadDTO, FacultyHead>();

            CreateMap<FacultyHead, FacultyHeadDTO>();

            CreateMap<ProfessionDTO, Profession>();
            CreateMap<NewProfession, Profession>();
            CreateMap<Profession, ProfessionDTO>();

            CreateMap<ScheduleDTO, Schedule>();
            CreateMap<Schedule, ScheduleDTO>();
            CreateMap<NewCourseSchedule, Schedule>();
            CreateMap<ScheduleDTO, ScheduleClass>()
                .ForMember(d => d.Exists, opt => opt.MapFrom(s => true))
                .ForMember(d => d.Subject, opt => opt.MapFrom(s => s.Subject.Name))
                .ForMember(d => d.Teacher, opt => opt.MapFrom(s => s.Teacher.FullName))
                .ForMember(d => d.Audience, opt => opt.MapFrom(s => s.Audience.Number));
            CreateMap<ScheduleDTO, ScheduleEditableClass>()
                .ForMember(d => d.SubjectId, opt => opt.MapFrom(s => s.Subject.Id))
                .ForMember(d => d.TeacherId, opt => opt.MapFrom(s => s.Teacher.Id))
                .ForMember(d => d.AudienceId, opt => opt.MapFrom(s => s.Audience.Id));

            CreateMap<StudentDTO, Student>();
            CreateMap<StudentDTO, AuthenticatedUser>()
                .ForMember(d => d.Role, opt => opt.MapFrom(s => UserRole.Student))
                .ForMember(d => d.IsFacultyHead, opt => opt.MapFrom(s => false))
                .ForMember(d => d.IsDepartmentHead, opt => opt.MapFrom(s => false));

            CreateMap<StudentSubjectDTO, StudentSubject>();

            CreateMap<SubjectDTO, Subject>();
            CreateMap<Subject, SubjectDTO>();
            CreateMap<NewSubject, Subject>();

            CreateMap<TeacherDTO, Teacher>();
            CreateMap<NewTeacher, Teacher>();
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
            CreateMap<JsonPatchDocument<TeacherShortInfo>, JsonPatchDocument<Teacher>>();
            CreateMap<Operation<TeacherShortInfo>, Operation<Teacher>>();

            CreateMap<TeacherSubjectDTO, TeacherSubject>();
            CreateMap<TeacherSubject, TeacherSubjectDTO>();
        }
    }
}
