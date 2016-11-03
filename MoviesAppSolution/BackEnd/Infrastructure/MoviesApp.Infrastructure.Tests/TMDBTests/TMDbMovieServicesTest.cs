using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesApp.Infrastructure.TMDb;
using MoviesApp.Services.ServiceFactory;
using MoviesApp.Services;
using MoviesApp.Services.Dto;
using MoviesApp.Domain.Model;
using Moq;

namespace MoviesApp.Infrastructure.Tests.TMDBTests
{
    [TestClass]
    public class TMDbMovieServicesTest
    {
        public readonly IService<IMovieSearch, PagedResult<IMovie>> getUpcomingMoviesService;
        public readonly IService<IMovieKey, IMovieDetails> getMovieDetailsService;

        public TMDbMovieServicesTest()
        {
            getUpcomingMoviesService = new TMDbMovieServices.GetMoviesService();
            getMovieDetailsService = new TMDbMovieServices.GetMovieDetailsService();
        }

        [TestMethod]
        public void ShouldGetUpcomingMovies()
        {
            var paginable = new Mock<IMovieSearch>();
            paginable.Setup(p => p.Page).Returns(1);

            var request = new Mock<IServiceRequest<IMovieSearch>>();
            request.Setup(r => r.Data).Returns(paginable.Object);

            var movies = getUpcomingMoviesService.ExecuteService(request.Object);
            Assert.IsNotNull(movies);
            Assert.IsNotNull(movies.Data);
            Assert.IsNotNull(movies.Data.Results);
            Assert.AreNotEqual(Guid.Empty, movies.ResponseKey);
            Assert.AreNotEqual(0, movies.Data.PageIndex);
            Assert.AreNotEqual(0, movies.Data.TotalResults);
            Assert.AreNotEqual(0, movies.Data.TotalPages);
            Assert.IsTrue(movies.Data.Results.Length > 0);
        }

        [TestMethod]
        public void ShouldSearchUpcomingMoviesByMovieName()
        {   
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ShouldGetMovieDetails()
        {
            Assert.Inconclusive();
        }
    }
}
