using ASU.Core.Database.Entities;
using ASU.Core.Database;
using ASU.Core.Services;
using AutoMapper;
using ASU.Core.DTO;

namespace ASU.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseTable<Student> _studentsTable;

        private const string ErrorNoTeacherFound = "No teacher found";

        public StudentsService(IDatabaseTable<Student> studentsTable, IMapper mapper)
        {
            _studentsTable = studentsTable;
            _mapper = mapper;
        }

        public async Task<StudentDTO> Get(int? id = null, string? email = null, bool throwException = false, bool includePassword = false)
        {

            var student = await _studentsTable.GetFirstAsync(t =>
                (id == null || t.Id == id) &&
                (email == null || t.Email == email));

            if (!includePassword)
            {
                student.Password = "";
            }

            if (student == null)
            {
                if (throwException)
                    throw new Exception(ErrorNoTeacherFound);
                return null;
            }

            return _mapper.Map<Student, StudentDTO>(student);
        }
    }
}
