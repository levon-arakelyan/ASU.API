using ASU.Core.Database;
using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using ASU.Infrastructure.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASU.Services
{
    public class SchedulesService : ISchedulesService
    {
        private readonly IDatabaseTable<Schedule> _schedulesTable;
        private readonly ICourseSubjectsService _courseSubjectsService;
        private readonly IMapper _mapper;

        private const string ErrorNoSchedule = "Ընտրված կուրսը դասացուցակ չունի";
        private const string ErrorWrongSchedule = "Դասացուցակը սխալ է կազմված";

        private const int MaxClassesPerDay = 4;
        private const int MaxRecordsForSingleClass = 2;
        private const int ClassNumbers = 4;

        private readonly List<DayOfWeek> WeekDays = new()
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday
        };

        public SchedulesService(
            IDatabaseTable<Schedule> schedulesTable,
            ICourseSubjectsService courseSubjectsService,
            IMapper mapper
        )
        {
            _schedulesTable = schedulesTable;
            _courseSubjectsService = courseSubjectsService;
            _mapper = mapper;
        }

        public async Task<ICollection<ScheduleDTO>> GetScheduleForCourse(int courseId)
        {
            var schedule = await GetQuery().Where(x => x.CourseId == courseId).ToListAsync();
            return _mapper.Map<ICollection<Schedule>, ICollection<ScheduleDTO>>(schedule);
        }

        public async Task<List<List<ScheduleClassGroup>>> GetRegularScheduleForCourse(int courseId)
        {
            var schedule = await GetScheduleForCourse(courseId);
            return TransformForRegularSchedule(schedule);
        }

        public async Task<List<List<ScheduleEditableClassGroup>>> GetEditableScheduleForCourse(int courseId)
        {
            var schedule = await GetScheduleForCourse(courseId);
            return TransformForEditableSchedule(schedule);
        }

        public async Task SaveScheduleForCourse(int courseId, ICollection<ICollection<ScheduleEditableClassGroup>> groups)
        {
            if (groups == null || !groups.Any() || groups.Any(x => !x.Any())) // TODO: add more checking
            {
                throw new BadRequestException(ErrorWrongSchedule);
            }

            List<Schedule> schedules = new();

            for (int i = 0; i < groups.Count; i++)
            {
                var row = groups.ElementAt(i);
                for (int j = 0; j < row.Count; j++)
                {
                    var group = row.ElementAt(j);
                    for (int k = 0; k < MaxRecordsForSingleClass; k++)
                    {
                        if (!group.Classes.Any())
                        {
                            continue;
                        }

                        var groupClass = group.Classes.ElementAt(group.ClassType == ScheduleClassType.Normal ? 0 : k);
                        if (groupClass.AudienceId <= 0 || groupClass.TeacherId <= 0 || groupClass.SubjectId <= 0)
                        {
                            continue;
                        }

                        schedules.Add(new Schedule()
                        {
                            CourseId = courseId,
                            SubjectId = groupClass.SubjectId,
                            TeacherId = groupClass.TeacherId,
                            AudienceId = groupClass.AudienceId,
                            DayOfWeek = (DayOfWeek)i,
                            ClassNumber = (ClassNumber)j,
                            StudentGroup = (StudentGroup)k,
                            IsFractionAbove = group.ClassType == ScheduleClassType.Fraction ? k == 0 : null
                        });
                    }
                }
            }

            if (_schedulesTable.Queryable().Any(x => x.CourseId == courseId))
            {
                _schedulesTable.BulkDeleteWhere(x => x.CourseId == courseId);
            }
            _schedulesTable.BulkAdd(schedules);
            await _schedulesTable.CommitAsync();

            await _courseSubjectsService.Save(schedules);
        }

        public async Task DeleteScheduleForCourse(int courseId)
        {
            var schedules = await _schedulesTable.Queryable().Where(x => x.CourseId == courseId).ToListAsync();
            if (schedules == null || !schedules.Any())
            {
                throw new BadRequestException(string.Format(ErrorNoSchedule, courseId));
            }

            _schedulesTable.BulkDelete(schedules);
            await _schedulesTable.CommitAsync();
        }

        private List<List<ScheduleEditableClassGroup>> TransformForEditableSchedule(ICollection<ScheduleDTO> schedule)
        {
            var classes = new List<List<ScheduleEditableClassGroup>>();
            if (!schedule.Any())
            {
                WeekDays.ForEach(x =>
                {
                    var row = new List<ScheduleEditableClassGroup>();
                    for (int i = 0; i < MaxClassesPerDay; i++)
                    {
                        row.Add(new ScheduleEditableClassGroup()
                        {
                            Classes = new List<ScheduleEditableClass>() { new ScheduleEditableClass() }
                        });
                    }
                    classes.Add(row);
                });
                return classes;
            }

            for (int i = 0; i < WeekDays.Count; i++)
            {
                classes.Add(new List<ScheduleEditableClassGroup>());
                var weekDaySubjects = schedule.Where(x => (int)x.DayOfWeek == i);
                var groupedByClassNumber = new List<List<ScheduleDTO>>();
                for (int j = 0; j < ClassNumbers; j++)
                {
                    var scheduleClass = weekDaySubjects.Where(x => (int)x.ClassNumber == j).ToList();
                    groupedByClassNumber.Add(scheduleClass);
                }

                groupedByClassNumber.ForEach(classesDto =>
                {
                    var first = classesDto.FirstOrDefault();
                    var second = classesDto.LastOrDefault();
                    if (first != null && second != null)
                    {
                        if (first == second)
                        {
                            classes.ElementAt(i).Add(new ScheduleEditableClassGroup()
                            {
                                ClassType = ScheduleClassType.Fraction,
                                Classes = first.IsFractionAbove != null && first.IsFractionAbove == true ?
                                    new List<ScheduleEditableClass>() { _mapper.Map<ScheduleDTO, ScheduleEditableClass>(first), new ScheduleEditableClass() } :
                                    new List<ScheduleEditableClass>() { new ScheduleEditableClass(), _mapper.Map<ScheduleDTO, ScheduleEditableClass>(first) }
                            });
                        }
                        else
                        {
                            var classType = ScheduleClassType.Group;
                            var scheduleClasses = new List<ScheduleEditableClass>
                            {
                                _mapper.Map<ScheduleDTO, ScheduleEditableClass>(first),
                                _mapper.Map<ScheduleDTO, ScheduleEditableClass>(second)
                            };

                            if (first.IsFractionAbove != null && second.IsFractionAbove != null)
                            {
                                classType = ScheduleClassType.Fraction;
                            }
                            else if (first.Subject.Id == second.Subject.Id && first.Teacher.Id == second.Teacher.Id && first.Audience.Id == second.Audience.Id)
                            {
                                classType = ScheduleClassType.Normal;
                                scheduleClasses = new List<ScheduleEditableClass>() { _mapper.Map<ScheduleDTO, ScheduleEditableClass>(first ?? second) };
                            }

                            classes.ElementAt(i).Add(new ScheduleEditableClassGroup()
                            {
                                ClassType = classType,
                                Classes = scheduleClasses
                            });
                        }
                    }
                    else
                    {
                        classes.ElementAt(i).Add(new ScheduleEditableClassGroup()
                        {
                            Classes = new List<ScheduleEditableClass>()
                        });
                    }
                });
            }

            classes.ForEach(row => row.ForEach(group => 
            {
                if (!group.Classes.Any())
                {
                    group.Classes = new List<ScheduleEditableClass>() { new ScheduleEditableClass() };
                }
            }));

            return classes;
        }

        private List<List<ScheduleClassGroup>> TransformForRegularSchedule(ICollection<ScheduleDTO> schedule)
        {
            var classes = new List<List<ScheduleClassGroup>>();
            for (int i = 0; i < WeekDays.Count; i++)
            {
                classes.Add(new List<ScheduleClassGroup>());
                var weekDaySubjects = schedule.Where(x => (int)x.DayOfWeek == i);
                var groupedByClassNumber = new List<List<ScheduleDTO>>();
                for (int j = 0; j < ClassNumbers; j++)
                {
                    var scheduleClass = weekDaySubjects.Where(x => (int)x.ClassNumber == j).ToList();
                    groupedByClassNumber.Add(scheduleClass);
                }

                groupedByClassNumber.ForEach(classesDto =>
                {
                    var first = classesDto.FirstOrDefault();
                    var second = classesDto.LastOrDefault();
                    if (first != null && second != null)
                    {
                        if (first == second)
                        {
                            classes.ElementAt(i).Add(new ScheduleClassGroup()
                            {
                                ClassType = ScheduleClassType.Fraction,
                                HasClasses = true,
                                Classes = first.IsFractionAbove != null && first.IsFractionAbove == true ?
                                    new List<ScheduleClass>() { _mapper.Map<ScheduleDTO, ScheduleClass>(first), new ScheduleClass() } :
                                    new List<ScheduleClass>() { new ScheduleClass(), _mapper.Map<ScheduleDTO, ScheduleClass>(first) }
                            });
                        }
                        else
                        {
                            var classType = ScheduleClassType.Group;
                            var scheduleClasses = new List<ScheduleClass>
                            {
                                _mapper.Map<ScheduleDTO, ScheduleClass>(first),
                                _mapper.Map<ScheduleDTO, ScheduleClass>(second)
                            };

                            if (first.IsFractionAbove != null && second.IsFractionAbove != null)
                            {
                                classType = ScheduleClassType.Fraction;
                            }
                            else if (first.Subject.Id == second.Subject.Id && first.Teacher.Id == second.Teacher.Id && first.Audience.Id == second.Audience.Id)
                            {
                                classType = ScheduleClassType.Normal;
                                scheduleClasses = new List<ScheduleClass>() { _mapper.Map<ScheduleDTO, ScheduleClass>(first ?? second) };
                            }

                            classes.ElementAt(i).Add(new ScheduleClassGroup()
                            {
                                ClassType = classType,
                                HasClasses = true,
                                Classes = scheduleClasses
                            });
                        }
                    }
                    else
                    {
                        classes.ElementAt(i).Add(new ScheduleClassGroup()
                        {
                            HasClasses = false,
                            Classes = new List<ScheduleClass>()
                        });
                    }
                });
            }

            return classes;
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
