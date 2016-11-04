using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesApp.Infrastructure.TMDb;
using MoviesApp.Services;
using MoviesApp.Domain.Model;
using Moq;
using System.Linq;

namespace MoviesApp.Infrastructure.Tests.TMDbTests
{
    [TestClass]
    public class TMDbMovieServicesTest
    {
        public readonly IService<IMovieSearch, PagedResult<IMovie>> getMoviesService;
        public readonly IService<IMovieKey, IMovieDetails> getMovieDetailsService;

        public TMDbMovieServicesTest()
        {
            getMoviesService = new TMDbMovieServices.GetMoviesService();
            getMovieDetailsService = new TMDbMovieServices.GetMovieDetailsService();
        }

        [TestMethod]
        public void ShouldGetMovies()
        {
            var search = new Mock<IMovieSearch>();
            search.Setup(s => s.Page).Returns(1);

            var request = new Mock<IServiceRequest<IMovieSearch>>();
            request.Setup(r => r.Data).Returns(search.Object);

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
            var search = new Mock<IMovieSearch>();
            search.Setup(s => s.Page).Returns(1);
            search.Setup(s => s.MovieName).Returns("Wind");

            var request = new Mock<IServiceRequest<IMovieSearch>>();
            request.Setup(r => r.Data).Returns(search.Object);

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
            var movieKey = new Mock<IMovieKey>();
            movieKey.Setup(k => k.MovieID).Returns(550);

            var request = new Mock<IServiceRequest<IMovieKey>>();
            request.Setup(r => r.Data).Returns(movieKey.Object);

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
