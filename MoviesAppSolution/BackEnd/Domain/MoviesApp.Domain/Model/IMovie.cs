using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Model
{
    public interface IMovie : IMovieKey
    {
        string Genre { get; set; }

        string PosterImage { get; set; }

        DateTime ReleaseDate { get; set; }
    }
}
