using ASU.Core.Database;
using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ASU.Services
{
    public class SchedulesService : ISchedulesService
    {
        private readonly IDatabaseTable<Schedule> _schedulesTable;
        private readonly ITeachersService _teachersService;
        private readonly IAudienciesService _audienciesService;
        private readonly ICoursesService _coursesService;
        private readonly ISubjectsService _subjectsService;
        private readonly IMapper _mapper;

        private const int maxClassesPerDay = 4;

        private readonly List<DayOfWeek> weekDays = new()
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday
        };

        public SchedulesService(
            IDatabaseTable<Schedule> schedulesTable,
            ITeachersService teachersService,
            IAudienciesService audienciesService,
            ICoursesService coursesService,
            ISubjectsService subjectsService,
            IMapper mapper
        )
        {
            _schedulesTable = schedulesTable;
            _teachersService = teachersService;
            _audienciesService = audienciesService;
            _coursesService = coursesService;
            _subjectsService = subjectsService;
            _mapper = mapper;
        }

        public async Task<ICollection<ScheduleDTO>> GetForCourse(int courseId)
        {
            var schedule = await GetQuery().Where(x => x.CourseId == courseId).ToListAsync();
            return _mapper.Map<ICollection<Schedule>, ICollection<ScheduleDTO>>(schedule);
        }

        public async Task<ICollection<ScheduleDTO>> GenerateScheduleForCourse(int courseId, ICollection<SubjectForSchedule> subjects)
        {
            var audiencies = await _audienciesService.GetAll();
            List<ScheduleDTO> schedule = new List<ScheduleDTO>();

            var repeatitions = subjects.Select(x => x.Repeat).Sum();
            if (repeatitions > weekDays.Count * maxClassesPerDay)
            {
                return null;
            }

            var weekDayIndex = 0;
            var recordedSubjects = new Dictionary<int, int>();
            for(int i = 0; i < repeatitions; i++)
            {
                var weekDay = weekDays[weekDayIndex];

                var subjectId = subjects.FirstOrDefault()?.SubjectId;
                if (subjectId == null)
                {
                    break;
                }
                var subject = await _subjectsService.Get((int)subjectId);
                var teachers = await _teachersService.GetBySubjectId(subject.Id);
                if (teachers == null)
                {
                    return null;
                }
                var teacher = teachers.FirstOrDefault();
                var audience = audiencies.FirstOrDefault(x => x.Type == subjects.FirstOrDefault()?.AudienceType);

                schedule.Add(new ScheduleDTO()
                {
                    Subject = subject,
                    Audience = audience,
                    Teacher = teacher,
                    DayOfWeek = weekDay,
                    ClassNumber = (ClassNumber)(i / weekDays.Count)
                });

                var first = subjects.First();
                first.Repeat--;
                if (first.Repeat == 0)
                {
                    subjects = subjects.Skip(1).ToList();
                }

                weekDayIndex = (weekDayIndex + 1) % weekDays.Count;
            }

           return schedule;
        }

        public async Task AddForCourse(int courseId, ICollection<NewCourseSchedule> schedules)
        {
            var course = await _coursesService.Get(courseId);
            var schedulesToAdd = new List<Schedule>();

            foreach(var schedule in schedules)
            {
                var scheduleEntity = _mapper.Map<NewCourseSchedule, Schedule>(schedule);
                scheduleEntity.CourseId = courseId;
                scheduleEntity.StudentGroup = StudentGroup.First;
                schedulesToAdd.Add(scheduleEntity);

                for (int i = 1; i < course.GroupsNumber; i++)
                {
                    var scheduleClone = _mapper.Map<NewCourseSchedule, Schedule>(schedule);
                    scheduleClone.CourseId = courseId;
                    scheduleClone.StudentGroup = (StudentGroup)(i);
                    schedulesToAdd.Add(scheduleClone);
                }

            }

            _schedulesTable.BulkAdd(schedulesToAdd);
            await _schedulesTable.CommitAsync();
        }


        private IQueryable<Schedule> GetQuery()
        {
            return _schedulesTable
                .Queryable()
                .Include(x => x.Teacher)
                .Include(x => x.Audience)
                .Include(x => x.Subject);
        }
    }
}
