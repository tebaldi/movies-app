using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.Dto
{
    public class MovieSearch : IDataTransferObject
    {
        public string MovieName { get; set; }

        public int Page { get; set; }

        public bool Upcoming { get { return String.IsNullOrEmpty(MovieName); } }
    }
}
