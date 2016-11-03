using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesApp.Infrastructure.TMDb;
using Newtonsoft.Json.Linq;

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
            var total_pages = response["total_pages"];
            var total_results = response["total_results"];
            var results = response["results"] as Newtonsoft.Json.Linq.JArray;

            Assert.IsNotNull(page);
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

        [TestMethod]
        public void ShouldCreateSearchMovieUri()
        {
            var uri = TMDbApi.SearchMovie.CreateUri("wind", 1);
            Assert.AreEqual("https://api.themoviedb.org/3/search/movie?api_key=1f54bd990f1cdfb230adb312546d765d&language=en-US&query=wind&page=1", uri);

            var response = TMDbApi.MakeApiRequest(uri);
            Assert.IsNotNull(response);

            var page = response["page"];
            var total_pages = response["total_pages"];
            var total_results = response["total_results"];
            var results = response["results"] as JArray;

            Assert.IsNotNull(page);
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

        [TestMethod]
        public void ShouldGetMovieDetailsUri()
        {
            var uri = TMDbApi.MovieDetails.CreateUri(550);
            Assert.AreEqual("https://api.themoviedb.org/3/movie/550?api_key=1f54bd990f1cdfb230adb312546d765d&language=en-US", uri);

            var response = TMDbApi.MakeApiRequest(uri);
            Assert.IsNotNull(response);

            var id = response["id"];
            var poster_path = response["poster_path"];
            var overview = response["overview"];
            var release_date = response["release_date"];
            var title = response["title"];
            var genres = response["genres"];

            Assert.IsNotNull(id);
            Assert.IsNotNull(poster_path);
            Assert.IsNotNull(overview);
            Assert.IsNotNull(release_date);
            Assert.IsNotNull(title);
            Assert.IsNotNull(genres);
            Assert.AreEqual(550, int.Parse(id.ToString()));
        }

        [TestMethod]
        public void ShouldGetGenres()
        {
            var uri = TMDbApi.Genres.CreateUri();
            Assert.AreEqual("https://api.themoviedb.org/3/genre/movie/list?api_key=1f54bd990f1cdfb230adb312546d765d&language=en-US", uri);

            var response = TMDbApi.MakeApiRequest(uri);
            Assert.IsNotNull(response);

            var genres = response["genres"] as JArray;
            Assert.IsNotNull(genres);

            foreach(var genre in genres)
            {
                var id = genre["id"];
                var name = genre["name"];

                Assert.IsNotNull(id);
                Assert.IsNotNull(name);
            }
        }

        [TestMethod]
        public void ShouldGetMovieImages()
        {
            var uri = TMDbApi.MovieImages.CreateUri(550);
            Assert.AreEqual("https://api.themoviedb.org/3/movie/550/images?api_key=1f54bd990f1cdfb230adb312546d765d", uri);

            var response = TMDbApi.MakeApiRequest(uri);
            Assert.IsNotNull(response);

            var id = response["id"];
            var backdrops = response["backdrops"];
            var posters = response["posters"];
            Assert.IsNotNull(id);
            Assert.IsNotNull(backdrops);
            Assert.IsNotNull(posters);

            foreach (var backdrop in backdrops)
            {
                var file_path = backdrop["file_path"];
                Assert.IsNotNull(file_path);
            }

            foreach (var poster in posters)
            {
                var file_path = poster["file_path"];
                Assert.IsNotNull(file_path);
            }
        }

        [TestMethod]
        public void SholdLoadImageLogoSize()
        {
            var uri = TMDbApi.LoadImage.CreateUri("/kqjL17yufvn9OVLyXYpvtyrFfak.jpg", true);
            Assert.AreEqual("http://image.tmdb.org/t/p/w154/kqjL17yufvn9OVLyXYpvtyrFfak.jpg", uri);
        }

        [TestMethod]
        public void SholdLoadImageNormalSize()
        {
            var uri = TMDbApi.LoadImage.CreateUri("/kqjL17yufvn9OVLyXYpvtyrFfak.jpg", false);
            Assert.AreEqual("http://image.tmdb.org/t/p/w500/kqjL17yufvn9OVLyXYpvtyrFfak.jpg", uri);
        }
    }
}
