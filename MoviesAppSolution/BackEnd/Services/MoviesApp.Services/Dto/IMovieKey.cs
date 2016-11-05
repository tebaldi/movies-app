using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.Dto
{
    public interface IMovieKey : IDataTransferObject
    {
        int MovieID { get; set; }
    }
}
