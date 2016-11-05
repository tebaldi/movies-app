using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesApp.Infrastructure.TMDb;
using MoviesApp.Services;
using MoviesApp.Services.Dto;
using Moq;
using System.Linq;

namespace MoviesApp.Infrastructure.Tests.TMDbTests
{
    [TestClass]
    public class TMDbMovieServicesTest
    {
        public readonly IService<MovieSearch, PagedResult<Movie>> getMoviesService;
        public readonly IService<MovieKey, MovieDetails> getMovieDetailsService;

        public TMDbMovieServicesTest()
        {
            getMoviesService = new TMDbMovieServices.GetMoviesService();
            getMovieDetailsService = new TMDbMovieServices.GetMovieDetailsService();
        }

        [TestMethod]
        public void ShouldGetMovies()
        {
            var search = new MovieSearch
            {
                Page = 1
            };

            var request = new Mock<IServiceRequest<MovieSearch>>();
            request.Setup(r => r.Data).Returns(search);

            var response = getMoviesService.ExecuteService(request.Object);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Data);
            Assert.IsNotNull(response.Data.Results);
            Assert.IsTrue(response.Data.Results.Length > 0);
            Assert.AreNotEqual(default(Guid), response.ResponseKey);
            Assert.AreNotEqual(default(int), response.Data.PageIndex);
            Assert.AreNotEqual(default(int), response.Data.TotalResults);
            Assert.AreNotEqual(default(int), response.Data.TotalPages);
            Assert.AreNotEqual(default(int), response.Data.Results.First().MovieID);
            Assert.AreNotEqual(default(string), response.Data.Results.First().MovieName);
            Assert.AreNotEqual(default(string), response.Data.Results.First().ImagePath);
            Assert.AreNotEqual(default(string), response.Data.Results.First().Genre);
            Assert.AreNotEqual(default(DateTime), response.Data.Results.First().ReleaseDate);
        }

        [TestMethod]
        public void ShouldSearchMoviesByMovieName()
        {
            var search = new MovieSearch()
            {
                Page = 1,
                MovieName = "Wind"
            };

            var request = new Mock<IServiceRequest<MovieSearch>>();
            request.Setup(r => r.Data).Returns(search);

            var response = getMoviesService.ExecuteService(request.Object);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Data);
            Assert.IsNotNull(response.Data.Results);
            Assert.IsTrue(response.Data.Results.Length > 0);
            Assert.AreNotEqual(default(Guid), response.ResponseKey);
            Assert.AreNotEqual(default(int), response.Data.PageIndex);
            Assert.AreNotEqual(default(int), response.Data.TotalResults);
            Assert.AreNotEqual(default(int), response.Data.TotalPages);

            var movie = response.Data.Results.First();
            Assert.AreNotEqual(default(int), movie.MovieID);
            Assert.AreNotEqual(default(string), movie.MovieName);
            Assert.AreNotEqual(default(string), movie.ImagePath);
            Assert.AreNotEqual(default(string), movie.Genre);
            Assert.IsTrue("Wind".Contains(movie.MovieName));
        }

        [TestMethod]
        public void ShouldGetMovieDetails()
        {
            var movieKey = new MovieKey
            {
                MovieID = 550
            };

            var request = new Mock<IServiceRequest<MovieKey>>();
            request.Setup(r => r.Data).Returns(movieKey);

            var response = getMovieDetailsService.ExecuteService(request.Object);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Data);
            Assert.AreNotEqual(default(Guid), response.ResponseKey);
            Assert.AreNotEqual(default(int), response.Data.MovieID);
            Assert.AreNotEqual(default(string), response.Data.MovieName);
            Assert.AreNotEqual(default(string), response.Data.OverView);
            Assert.AreNotEqual(default(string), response.Data.ImagePath);
            Assert.AreNotEqual(default(string), response.Data.Genre);
            Assert.AreNotEqual(default(DateTime), response.Data.ReleaseDate);
        }
    }
}
