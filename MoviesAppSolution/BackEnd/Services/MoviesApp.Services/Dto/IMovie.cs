using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.Dto
{
    public interface IMovie : IMovieKey
    {
        string PosterImage { get; set; }

        string Genre { get; set; }

        DateTime ReleaseDate { get; set; }
    }
}
