using ASU.Core.Database;
using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Services;
using AutoMapper;

namespace ASU.Services
{
    public class DepartmentHeadsService : IDepartmentHeadsService
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseTable<DepartmentHead> _departmentHeadsTable;
        private const string ErrorNoTeacherGiven = "No data given to add.";

        public DepartmentHeadsService(IDatabaseTable<DepartmentHead> departmentHeadsTable, IMapper mapper)
        {
            _departmentHeadsTable = departmentHeadsTable;
            _mapper = mapper;
        }

        public async Task Add(TeacherDTO teacher)
        {
            if (teacher == null)
            {
                throw new ArgumentNullException(ErrorNoTeacherGiven);
            }

            var head = _mapper.Map<TeacherDTO, DepartmentHead>(teacher);
            await _departmentHeadsTable.AddAsync(head);
            await _departmentHeadsTable.CommitAsync();
        }
    }
}
