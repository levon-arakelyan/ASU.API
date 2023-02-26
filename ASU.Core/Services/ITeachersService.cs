using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;

namespace ASU.Core.Services
{
    public interface ITeachersService
    {
        Task<TeacherDTO> Get(int? id = null, string? email = null, bool throwException = false, bool includePassword = false);
        Task<ICollection<TeacherDTO>> GetBySubjectId(int subjectId);
        Task Add(NewTeacher teacherDto);
        PagedItemsList<TeacherDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "");
        Task Edit(int teacherId, EditTeacher teacher);
        Task<ICollection<TeacherDTO>> GetBySubject(int subjectId);
        Task<ICollection<TeacherDTO>> GetAll();
    }
}
