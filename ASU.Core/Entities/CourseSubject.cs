﻿using ASU.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ASU.Core.Entities
{
    public class CourseSubject : AuditableUtcEntity
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int Credit { get; set; }
        public virtual Course Course { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
