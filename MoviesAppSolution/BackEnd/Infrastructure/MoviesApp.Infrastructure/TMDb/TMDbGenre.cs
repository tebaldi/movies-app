using MoviesApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MoviesApp.Infrastructure.TMDb
{
    class TMDbGenre 
    {
        private static List<TMDbGenre> sCache = new List<TMDbGenre>();

        public TMDbGenre(JToken token)
        {
            ID = int.Parse(token["id"].ToString());
            Name = token["name"].ToString();
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public static List<TMDbGenre> LoadGenres()
        {
            if (!sCache.Any())
            {
                var genresResonse = TMDbApi.MakeApiRequest(TMDbApi.Genres.CreateUri());
                var genres = genresResonse["genres"] as JArray;

                sCache.AddRange(MapGenres(genres));
            }

            return sCache;
        }

        public static IEnumerable<TMDbGenre> MapGenres(JArray genres)
        {
            foreach (var genre in genres)
                yield return new TMDbGenre(genre);
        }
    }
}
