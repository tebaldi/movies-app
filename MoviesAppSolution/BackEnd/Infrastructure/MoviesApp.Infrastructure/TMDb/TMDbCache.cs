using MoviesApp.Infrastructure.TMDb;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Infrastructure.TMDb
{
    public class TMDbCache
    {
        private static string _baseImagesUrl;
        public static string BaseImagesUrl
        {
            get
            {
                if (String.IsNullOrEmpty(_baseImagesUrl))
                {
                    var configUri = TMDbApi.Configuration.CreateUri();
                    var configResponse = TMDbApi.MakeApiRequest(configUri);
                    var images = configResponse["images"];

                    _baseImagesUrl = images["base_url"].ToString();
                }

                return _baseImagesUrl;
            }
        }

        private static readonly List<TMDbGenre> _genresCache = new List<TMDbGenre>();
        public static List<TMDbGenre> Genres
        {
            get
            {
                if (!_genresCache.Any())
                {
                    var genresResonse = TMDbApi.MakeApiRequest(TMDbApi.Genres.CreateUri());
                    var genres = genresResonse["genres"] as JArray;

                    _genresCache.AddRange(TMDbGenre.MapGenres(genres));
                }

                return _genresCache;
            }
        }
    }
}
