using ASU.Core.Database.Entities;
using ASU.Core.Database;
using ASU.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASU.Services
{
    public class TeacherSubjectsService : ITeacherSubjectsService
    {
        private readonly IDatabaseTable<TeacherSubject> _teacherSubjectsTable;
        private readonly IMapper _mapper;

        public TeacherSubjectsService(IDatabaseTable<TeacherSubject> teacherSubjectsTable, IMapper mapper)
        {
            _teacherSubjectsTable = teacherSubjectsTable;
            _mapper = mapper;
        }

        public async Task EditSubjects(int teacherId, ICollection<int> subjectsIds)
        {
            var allSubjects = await GetQuery()
                .Where(x => x.TeacherId == teacherId)
                .ToListAsync();

            var allSubjectsIds = allSubjects.Select(x => x.SubjectId);
            var subjectsToAdd = subjectsIds.Where(id => !allSubjectsIds.Contains(id)).Select(id => new TeacherSubject()
            {
                TeacherId = teacherId,
                SubjectId = id
            });

            var subjectsToRemove = allSubjects.Where(x => !subjectsIds.Contains(x.SubjectId));

            _teacherSubjectsTable.BulkAdd(subjectsToAdd);
            _teacherSubjectsTable.BulkDelete(subjectsToRemove);
            await _teacherSubjectsTable.CommitAsync();
        }

        private IQueryable<TeacherSubject> GetQuery()
        {
            return _teacherSubjectsTable
                .Queryable();
        }
    }
}
