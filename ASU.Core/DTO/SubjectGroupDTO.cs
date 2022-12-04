namespace ASU.Core.DTO
{
    public class SubjectGroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SubjectDTO> Subjects { get; set; }
    }
}
