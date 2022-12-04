using ASU.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASU.Core.Models
{
    public class TeacherShortInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double Rate { get; set; }
        public TeacherDegree Degree { get; set; }
        public int DepartmentId { get; set; }
    }
}
