using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.Dto
{
    public interface IMovie : IMovieKey
    {
        string MovieName { get; set; }

        string Genre { get; set; }

        string ImagePath { get; set; }

        DateTime ReleaseDate { get; set; }
    }
}
