
namespace Absa.Models
{
    public class PagingFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int SkipLength => (PageNumber - 1) * PageSize;
    }
}
