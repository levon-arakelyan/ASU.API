using ASU.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASU.Database
{
    public class ASUContext : DbContext
    {
        public ASUContext(DbContextOptions<ASUContext> options) : base(options)
        { }

        public virtual DbSet<Audience> Audiences { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentHead> DepartmentHeads { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<FacultyHead> FacultyHeads { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<CourseSubject> CourseSubjects { get; set; }
        public virtual DbSet<StudentSubject> StudentSubjects { get; set; }
        public virtual DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<SubjectGroup> SubjectGroups { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audience>(t =>
            {
                t.ToTable("Audiences", "dbo");
                t.HasKey(_ => _.Id);
                t.HasMany(_ => _.Schedule).WithOne(_ => _.Audience).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Course>(t =>
            {
                t.ToTable("Courses", "dbo");
                t.HasKey(_ => _.Id);
                t.HasOne(_ => _.Profession).WithMany(_ => _.Courses).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(_ => _.Students).WithOne(_ => _.Course).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(_ => _.CourseSubjects).WithOne(_ => _.Course).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(_ => _.Schedule).WithOne(_ => _.Course).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Department>(t =>
            {
                t.ToTable("Departments", "dbo");
                t.HasKey(_ => _.Id);
                t.HasOne(_ => _.Faculty).WithMany(_ => _.Departments).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(_ => _.Professions).WithOne(_ => _.Department).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.DepartmentHead).WithOne(_ => _.Department).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DepartmentHead>(t =>
            {
                t.ToTable("DepartmentHeads", "dbo");
                t.HasKey(_ => _.Id);
                t.HasOne(_ => _.Department).WithOne(_ => _.DepartmentHead).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.Head).WithOne(_ => _.DepartmentHead).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Faculty>(t =>
            {
                t.ToTable("Faculties", "dbo");
                t.HasKey(_ => _.Id);
                t.HasMany(_ => _.Departments).WithOne(_ => _.Faculty).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.FacultyHead).WithOne(_ => _.Faculty).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<FacultyHead>(t =>
            {
                t.ToTable("FacultyHeads", "dbo");
                t.HasKey(_ => _.Id);
                t.HasOne(_ => _.Faculty).WithOne(_ => _.FacultyHead).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.Head).WithOne(_ => _.FacultyHead).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Profession>(t =>
            {
                t.ToTable("Professions", "dbo");
                t.HasKey(_ => _.Id);
                t.HasOne(_ => _.Department).WithMany(_ => _.Professions).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(_ => _.Courses).WithOne(_ => _.Profession).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Student>(t =>
            {
                t.ToTable("Students", "dbo");
                t.HasKey(_ => _.Id);
                t.HasOne(_ => _.Course).WithMany(_ => _.Students).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(_ => _.StudentSubjects).WithOne(_ => _.Student).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Teacher>(t =>
            {
                t.ToTable("Teachers", "dbo");
                t.HasKey(_ => _.Id);
                t.HasOne(_ => _.Department).WithMany(_ => _.Teachers).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(_ => _.TeacherSubjects).WithOne(_ => _.Teacher).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(_ => _.CourseSubjects).WithOne(_ => _.Teacher).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(_ => _.Schedule).WithOne(_ => _.Teacher).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.DepartmentHead).WithOne(_ => _.Head).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.FacultyHead).WithOne(_ => _.Head).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Subject>(t =>
            {
                t.ToTable("Subjects", "dbo");
                t.HasKey(_ => _.Id);
                t.HasMany(_ => _.CourseSubjects).WithOne(_ => _.Subject).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(_ => _.StudentSubjects).WithOne(_ => _.Subject).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(_ => _.TeacherSubjects).WithOne(_ => _.Subject).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(_ => _.Schedule).WithOne(_ => _.Subject).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.SubjectGroup).WithMany(_ => _.Subjects).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CourseSubject>(t =>
            {
                t.ToTable("CourseSubjects", "dbo");
                t.HasKey(_ => _.Id);
                t.HasOne(_ => _.Course).WithMany(_ => _.CourseSubjects).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.Subject).WithMany(_ => _.CourseSubjects).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.Teacher).WithMany(_ => _.CourseSubjects).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<TeacherSubject>(t =>
            {
                t.ToTable("TeacherSubjects", "dbo");
                t.HasKey(_ => _.Id);
                t.HasOne(_ => _.Teacher).WithMany(_ => _.TeacherSubjects).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.Subject).WithMany(_ => _.TeacherSubjects).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<StudentSubject>(t =>
            {
                t.ToTable("StudentSubjects", "dbo");
                t.HasKey(_ => _.Id);
                t.HasOne(_ => _.Student).WithMany(_ => _.StudentSubjects).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.Subject).WithMany(_ => _.StudentSubjects).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.Course).WithMany(_ => _.StudentSubjects).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Schedule>(t =>
            {
                t.ToTable("Schedules", "dbo");
                t.HasKey(_ => _.Id);
                t.HasOne(_ => _.Course).WithMany(_ => _.Schedule).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.Teacher).WithMany(_ => _.Schedule).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.Subject).WithMany(_ => _.Schedule).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(_ => _.Audience).WithMany(_ => _.Schedule).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<SubjectGroup>(t =>
            {
                t.ToTable("SubjectGroups", "dbo");
                t.HasKey(_ => _.Id);
                t.HasMany(_ => _.Subjects).WithOne(_ => _.SubjectGroup).OnDelete(DeleteBehavior.NoAction);
            });

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faculty>().HasData(new Faculty()
            {
                Id = 1,
                Name = "Բնագիտական"
            });

            modelBuilder.Entity<Department>().HasData(new Department()
            {
                Id = 1,
                FacultyId = 1,
                Name = "Կիրառական մաթեմատիկա և ինֆորմատիկա"
            });

            modelBuilder.Entity<Teacher>().HasData(new Teacher()
            {
                Id = 1,
                FirstName = "Վոլոդյա",
                LastName = "Միրզոյան",
                Email = "volodyamirzoyan@mail.ru",
                Password = "$2a$04$z3wvBnd4orVJ.mWvXYECeuCVi3qd/uhQLeqfzoSHujb6QOiy0q.Fm",
                Rate = 1,
                DepartmentId = 1
            });

            modelBuilder.Entity<FacultyHead>().HasData(new FacultyHead()
            {
                Id = 1,
                FacultyId = 1,
                TeacherId = 1
            });
        }
    }
}