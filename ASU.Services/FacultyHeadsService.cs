using ASU.Core.Database;
using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASU.Services
{
    public class FacultyHeadsService : IFacultyHeadsService
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseTable<FacultyHead> _facultyHeadsTable;
        private const string ErrorFacultyHeadNotExist = "{0} ֆակուլտետը չունի դեկան";
        private const string ErrorFacultyHasHead = "{0} ֆակուլտետը արդեն ունի դեկան.";
        private const string ErrorNoFacultyWithProvidedHead = "{0} ֆակուլտետի դեկանը {0}-ը չէ";

        public FacultyHeadsService(IDatabaseTable<FacultyHead> facultyHeadsTable, IMapper mapper)
        {
            _facultyHeadsTable = facultyHeadsTable;
            _mapper = mapper;
        }

        public async Task TryEdit(int facultyId, int teacherId, bool add)
        {
            var facultyHead = GetQuery().FirstOrDefault(x => x.FacultyId == facultyId);

            if (facultyHead == null && add)
            {
                await Add(facultyId, teacherId);
            }
            else if (facultyHead != null && !add)
            {
                await Remove(facultyId, teacherId);
            }
        }

        private async Task Add(int facultyId, int teacherId)
        {
            var head = new FacultyHead()
            {
                FacultyId = facultyId,
                TeacherId = teacherId
            };
            await _facultyHeadsTable.AddAsync(head);
            await _facultyHeadsTable.CommitAsync();
        }

        private async Task Remove(int facultyId, int teacherId)
        {
            var facultyHead = GetQuery().FirstOrDefault(x => x.FacultyId == facultyId && x.TeacherId == teacherId);
            if (facultyHead != null)
            {
                await _facultyHeadsTable.DeleteAsync(facultyHead);
            }
        }

        private IQueryable<FacultyHead> GetQuery()
        {
            return _facultyHeadsTable
                .Queryable()
                .Include(x => x.Faculty)
                .Include(x => x.Head);
        }
    }
}
