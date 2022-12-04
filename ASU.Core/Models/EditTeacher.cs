using Microsoft.AspNetCore.JsonPatch;

namespace ASU.Core.Models
{
    public class EditTeacher
    {
        public JsonPatchDocument<TeacherShortInfo>? TeacherShortInfoPatch { get; set; }
        public bool IsFacultyHead { get; set; }
        public bool IsDepartmentHead { get; set; }
        public int[] SubjectIds { get; set; }
    }
}
