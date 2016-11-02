using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesApp.Infrastructure.TMDb;

namespace MoviesApp.Infrastructure.Tests.TMDBTests
{
    [TestClass]
    public class TMDbApiMapTest
    {
        [TestMethod]
        public void ShouldCreateGetUpcomingMoviesUri()
        {
            var uri = TMDbApi.UpcomingMovies.CreateUri(1);
            Assert.AreEqual("https://api.themoviedb.org/3/movie/upcoming?api_key=1f54bd990f1cdfb230adb312546d765d&language=en-US&page=1", uri);

            var response = TMDbApi.MakeApiRequest(uri);
            Assert.IsNotNull(response);

            var page = response["page"];
            var dates = response["dates"];
            var total_pages = response["total_pages"];
            var total_results = response["total_results"];
            var results = response["results"] as Newtonsoft.Json.Linq.JArray;
            
            foreach(var r in results)
            {
                var id = r["id"];
                var poster_path = r["poster_path"];
                var overview = r["overview"];
                var release_date = r["release_date"];
                var title = r["title"];
            }

            Assert.AreEqual(1, response["page"]);            
        }
    }
}
