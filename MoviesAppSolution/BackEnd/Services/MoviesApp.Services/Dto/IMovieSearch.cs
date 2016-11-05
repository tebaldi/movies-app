using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.Dto
{
    public interface IMovieSearch : IDataTransferObject
    {
        string MovieName { get; }

        int Page { get; }
    }
}
