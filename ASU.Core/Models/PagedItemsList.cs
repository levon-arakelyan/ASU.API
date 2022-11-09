namespace ASU.Core.Models
{
    public class PagedItemsList<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public PagedListOrder Order { get; set; }
        public ICollection<T> Data { get; set; }
    }
}
