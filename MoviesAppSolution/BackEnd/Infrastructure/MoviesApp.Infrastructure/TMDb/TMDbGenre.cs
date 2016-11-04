using MoviesApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MoviesApp.Infrastructure.TMDb
{
    public class TMDbGenre
    {
        public TMDbGenre(JToken token)
        {
            ID = int.Parse(token["id"].ToString());
            Name = token["name"].ToString();
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public static IEnumerable<TMDbGenre> MapGenres(JArray genres)
        {
            foreach (var genre in genres)
                yield return new TMDbGenre(genre);
        }
    }
}
