using ASU.Core.Enums;

namespace ASU.Core.Models
{
    public class PagedListOrder
    {
        public string OrderBy { get; set; }
        public OrderDirection Direction { get; set; }
    }
}
