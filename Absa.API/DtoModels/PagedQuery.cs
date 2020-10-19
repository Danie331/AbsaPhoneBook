
namespace Absa.API.DtoModels
{
    public class PagedQuery
    {
        public PagedQuery() { }
        public PagedQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
}
