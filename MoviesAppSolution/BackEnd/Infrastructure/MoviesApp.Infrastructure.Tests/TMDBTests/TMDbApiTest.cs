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

            Assert.IsNotNull(page);
            Assert.IsNotNull(dates);
            Assert.IsNotNull(total_pages);
            Assert.IsNotNull(total_results);
            Assert.IsNotNull(results);

            foreach (var result in results)
            {
                var id = result["id"];
                var poster_path = result["poster_path"];
                var overview = result["overview"];
                var release_date = result["release_date"];
                var title = result["title"];
                var genre_ids = result["genre_ids"];

                Assert.IsNotNull(id);
                Assert.IsNotNull(poster_path);
                Assert.IsNotNull(overview);
                Assert.IsNotNull(release_date);
                Assert.IsNotNull(title);
                Assert.IsNotNull(genre_ids);
            }
        }
    }
}
