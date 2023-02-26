using ASU.Core.Database;
using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Models;
using ASU.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace ASU.Services
{
    public class CourseSubjectsService : ICourseSubjectsService
    {
        private readonly IDatabaseTable<CourseSubject> _courseSubjectsTable;
        private readonly IMapper _mapper;

        public CourseSubjectsService(IDatabaseTable<CourseSubject> courseSubjectsTable, IMapper mapper)
        {
            _courseSubjectsTable = courseSubjectsTable;
            _mapper = mapper;
        }

        public async Task Save(List<Schedule> schedule)
        {
            var distinctsBySubject = schedule.DistinctBy(x => x.SubjectId).ToList();
            var newCourseSubjects = new List<CourseSubject>();

            foreach (var courseSubject in distinctsBySubject)
            {
                var exists = await _courseSubjectsTable
                    .Queryable()
                    .AnyAsync(y => y.CourseId == courseSubject.CourseId && y.SubjectId == courseSubject.SubjectId);
                if (!exists)
                {
                    newCourseSubjects.Add(new CourseSubject()
                    {
                        SubjectId = courseSubject.SubjectId,
                        CourseId = courseSubject.CourseId,
                        Credit = 0
                    });
                }
            }

            var courseSubjectsToDelete = _courseSubjectsTable
                .Queryable()
                .AsEnumerable()
                .Where(x => !distinctsBySubject.Exists(y => y.SubjectId == x.SubjectId && y.CourseId == x.CourseId));
            _courseSubjectsTable.BulkAdd(newCourseSubjects);
            _courseSubjectsTable.BulkDelete(courseSubjectsToDelete);
            await _courseSubjectsTable.CommitAsync();
        }

        public async Task Save(int[] ids, JsonPatchDocument<ICollection<CourseSubjectDTO>> courseSubjectsPatch)
        {
            var courseSubjects = await _courseSubjectsTable.Queryable().Where(x => ids.Contains(x.Id)).ToListAsync();
            var patch = _mapper.Map<JsonPatchDocument<ICollection<CourseSubjectDTO>>, JsonPatchDocument<ICollection<CourseSubject>>>(courseSubjectsPatch);
            patch.ApplyTo(courseSubjects);
            _courseSubjectsTable.BulkUpdate(courseSubjects);
            await _courseSubjectsTable.CommitAsync();
        }

        public async Task<ICollection<CourseSubjectDTO>> GetForCourse(int courseId)
        {
            var response = await GetQuery().Where(x => x.CourseId == courseId).ToListAsync();
            return _mapper.Map<ICollection<CourseSubject>, ICollection<CourseSubjectDTO>>(response);
        }

        private IQueryable<CourseSubject> GetQuery()
        {
            return _courseSubjectsTable
                .Queryable()
                .Include(x => x.Subject)
                .Include(x => x.Course)
                .ThenInclude(x => x.Profession);
        }
    }
}
