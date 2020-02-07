using MoviesApp.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Infrastructure.TMDb
{
    internal class TMDbMovie : MovieDetails
    {
        public TMDbMovie(JToken token)
        {
            var genresArray = default(string[]);

            if (token["genre_ids"] != null)
            {
                var genre_ids = token["genre_ids"] as JArray;

                genresArray = TMDbCache.Genres
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
            OverView = token["overview"] != null ? token["overview"].ToString() : string.Empty;
            ImagePath = TMDbApi.ImagePath.CreateUri(token["poster_path"].ToString(), true);
            ReleaseDate = String.IsNullOrEmpty(token["release_date"].ToString())
                        ? default(DateTime)
                        : DateTime.Parse(token["release_date"].ToString());
        }
    }
}
