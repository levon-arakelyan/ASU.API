namespace ASU.Core.Services
{
    public interface ITeacherSubjectsService
    {
        Task EditSubjects(int teacherId, ICollection<int> subjectsIds);
    }
}
