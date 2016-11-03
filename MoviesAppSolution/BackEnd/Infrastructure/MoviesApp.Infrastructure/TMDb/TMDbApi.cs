using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Infrastructure.TMDb
{
    public class TMDbApi
    {
        private const string Key = "1f54bd990f1cdfb230adb312546d765d";
        private const string BaseUrl = "https://api.themoviedb.org/";
        private const int APIVersion = 3;

        public static JObject MakeApiRequest(string requestUri)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(requestUri).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var jsonObject = JsonConvert.DeserializeObject(result);
                return jsonObject as JObject;
            }
        }

        public class UpcomingMovies
        {
            private const string Action = "/movie/upcoming";
            private const string API_Key = "api_key";
            private const string Language = "language";
            private const string Page = "page";

            public static string CreateUri(int page)
            {
                var uri = $"{BaseUrl}{APIVersion}{Action}?{API_Key}={Key}&{Language}=en-US&{Page}={page}";
                return uri;
            }       
        }

        public class SearchMovie
        {
            private const string Action = "/search/movie";
            private const string API_Key = "api_key";
            private const string Language = "language";
            private const string Query = "query";
            private const string Page = "page";

            public static string CreateUri(string query, int page)
            {
                var uri = $"{BaseUrl}{APIVersion}{Action}?{API_Key}={Key}&{Language}=en-US&{Query}={query}&{Page}={page}";
                return uri;
            }
        }

        public class MovieDetails
        {
            private const string Action = "/movie/";
            private const string API_Key = "api_key";
            private const string Language = "language";

            public static string CreateUri(int id)
            {
                var uri = $"{BaseUrl}{APIVersion}{Action}{id}?{API_Key}={Key}&{Language}=en-US";
                return uri;
            }
        }

        public class Genres
        {
            private const string Action = "/genre/movie/list";
            private const string API_Key = "api_key";
            private const string Language = "language";

            public static string CreateUri()
            {
                var uri = $"{BaseUrl}{APIVersion}{Action}?{API_Key}={Key}&{Language}=en-US";
                return uri;
            }
        }

        public class MovieImages
        {
            private const string Action = "/movie/{movie_id}/images";
            private const string API_Key = "api_key";

            public static string CreateUri(int id)
            {
                var uri = $"{BaseUrl}{APIVersion}{Action}?{API_Key}={Key}"
                    .Replace("{movie_id}", id.ToString());
                return uri;
            }
        }

        public class LoadImage
        {
            private const string Configuration = "/configuration";
            private const string LogoSize = "w154";
            private const string NormalSize = "w500";
            private const string API_Key = "api_key";

            public static string CreateUri(string path, bool logoSize)
            {
                var configUri = $"{BaseUrl}{APIVersion}{Configuration}?{API_Key}={Key}";
                var configResponse = MakeApiRequest(configUri);
                var images = configResponse["images"];
                var base_url = images["base_url"].ToString();
                var size = logoSize ? LogoSize : NormalSize;

                var uri = $"{base_url}{size}{path}";
                return uri;
            }
        }
    }
}
