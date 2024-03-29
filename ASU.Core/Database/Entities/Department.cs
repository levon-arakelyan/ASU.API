﻿using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Database.Entities
{
    public class Department : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual ICollection<Profession> Professions { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual DepartmentHead DepartmentHead { get; set; }
    }
}
