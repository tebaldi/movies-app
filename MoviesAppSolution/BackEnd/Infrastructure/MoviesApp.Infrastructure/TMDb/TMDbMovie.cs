using MoviesApp.Domain.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Infrastructure.TMDb
{
    class TMDbMovie : IMovieDetails
    {
        public TMDbMovie(JToken token)
        {
            var genresArray = default(string[]);

            if (token["genre_ids"] != null)
            {
                var genre_ids = token["genre_ids"] as JArray;

                genresArray = TMDbGenre.LoadGenres()
                    .Where(g => genre_ids.Any(id => int.Parse(id.ToString()) == g.ID))
                    .Select(g => g.Name)
                    .ToArray();
            }
            else
            {
                var genres = token["genres"] as JArray;

                genresArray = TMDbGenre.MapGenres(genres)
                    .Select(g => g.Name)
                    .ToArray();
            }
            
            MovieID = int.Parse(token["id"].ToString());
            MovieName = token["title"].ToString();
            Genre = string.Join(", ", genresArray);
            ImagePath = token["poster_path"].ToString();
            ReleaseDate = String.IsNullOrEmpty(token["release_date"].ToString())
                        ? default(DateTime)
                        : DateTime.Parse(token["release_date"].ToString());
        }

        public int MovieID { get; set; }

        public string Genre { get; set; }

        public string MovieName { get; set; }

        public string OverView { get; set; }

        public string ImagePath { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
