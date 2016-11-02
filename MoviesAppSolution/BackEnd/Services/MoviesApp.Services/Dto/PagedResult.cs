using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.Dto
{
    public class PagedResult<T>
    {
        public T[] Results { get; set; }

        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int TotalResults { get; set; }
    }
}
