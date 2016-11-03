using MoviesApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Infrastructure.TMDb
{
    class TMDbMovie : IMovieDetails
    {
        public int MovieID { get; set; }

        public string Genre { get; set; }

        public string MovieName { get; set; }

        public string OverView { get; set; }

        public string ImagePath { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
