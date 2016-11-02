using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.Dto
{
    public class PagedResult<T> : IDataTransferObject
        where T: IDataTransferObject
    {
        public T[] Data { get; set; }

        public int TotalRecord { get; set; }

        public int TotalPages { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
