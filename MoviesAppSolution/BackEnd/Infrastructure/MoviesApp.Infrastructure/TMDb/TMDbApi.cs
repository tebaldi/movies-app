using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
    }
}
