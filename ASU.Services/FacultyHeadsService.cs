using ASU.Core.Database;
using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Services;
using AutoMapper;

namespace ASU.Services
{
    public class FacultyHeadsService : IFacultyHeadsService
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseTable<FacultyHead> _facultyHeadsTable;
        private const string ErrorNoTeacherGiven = "No data given to add.";

        public FacultyHeadsService(IDatabaseTable<FacultyHead> facultyHeadsTable, IMapper mapper)
        {
            _facultyHeadsTable = facultyHeadsTable;
            _mapper = mapper;
        }

        public async Task Add(TeacherDTO teacher)
        {
            if (teacher == null)
            {
                throw new ArgumentNullException(ErrorNoTeacherGiven);
            }

            var head = _mapper.Map<TeacherDTO, FacultyHead>(teacher);
            await _facultyHeadsTable.AddAsync(head);
            await _facultyHeadsTable.CommitAsync();
        }
    }
}
