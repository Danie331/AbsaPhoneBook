
using System.Collections.Generic;

namespace Absa.API.DtoModels
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data, int pageNumber, int pageSize)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public IEnumerable<T> Data { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
