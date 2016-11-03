using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Model
{
    public interface IMovieSearch
    {
        string MovieName { get; }

        int Page { get; }
    }
}
