using Microsoft.AspNetCore.JsonPatch;

namespace ASU.Core.Models
{
    public class EditCourse
    {
        public JsonPatchDocument<CourseShortInfo>? CourseShortInfoPatch { get; set; }
    }
}
