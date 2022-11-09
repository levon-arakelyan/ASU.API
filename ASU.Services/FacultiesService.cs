using ASU.Core.Database;
using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Services;
using AutoMapper;

namespace ASU.Services
{
    public class FacultiesService : IFacultiesService
    {
        private readonly IDatabaseTable<Faculty> _facultiesTable;
        private readonly IMapper _mapper;
        private const string ErrorNoTeacherGiven = "No data given to add.";

        public FacultiesService(IDatabaseTable<Faculty> facultiesTable, IMapper mapper)
        {
            _facultiesTable = facultiesTable;
            _mapper = mapper;
        }

        public async Task Add(FacultyDTO facultyDto)
        {
            if (facultyDto == null)
            {
                throw new ArgumentNullException(ErrorNoTeacherGiven);
            }

            var faculty = _mapper.Map<FacultyDTO, Faculty>(facultyDto);
            await _facultiesTable.AddAsync(faculty);
            await _facultiesTable.CommitAsync();
        }
    }
}
