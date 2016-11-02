using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesApp.Infrastructure.TMDb;
using MoviesApp.Services.ServiceFactory;
using MoviesApp.Services;
using MoviesApp.Services.Dto;

namespace MoviesApp.Infrastructure.Tests.TMDBTests
{
    [TestClass]
    public class TMDbMovieServicesTest
    {
        public readonly IService<PagedResult<IMovie>> getAllMoviesService;
        public readonly IService<IMovieKey, IMovieDetails> getMovieDetailsService;
        public readonly IService<IMovieKey, PagedResult<IMovie>> searchMoviesService;

        public TMDbMovieServicesTest()
        {
            getAllMoviesService = new TMDbMovieServices.GetAllMoviesService();
            getMovieDetailsService = new TMDbMovieServices.GetMovieDetailsService();
            searchMoviesService = new TMDbMovieServices.SearchMoviesService();
        }

        [TestMethod]
        public void ShouldGetAllMovies()
        {
            var allMovies = getAllMoviesService.ExecuteService();
            Assert.IsNotNull(allMovies);
            Assert.IsNotNull(allMovies.Data);
            Assert.IsNotNull(allMovies.Data.Result);
            Assert.AreNotEqual(Guid.Empty, allMovies.ResponseKey);
            Assert.AreNotEqual(0, allMovies.Data.TotalRecords);
            Assert.AreNotEqual(0, allMovies.Data.PageIndex);
            Assert.AreNotEqual(0, allMovies.Data.PageSize);
            Assert.IsTrue(allMovies.Data.Result.Length > 0);
        }

        [TestMethod]
        public void ShouldCreateGetMovieDetailsService()
        {
            var service = factory.CreateGetMovieDetailsService();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(TMDbMovieServices.GetMovieDetailsService));
        }

        [TestMethod]
        public void ShouldCreateSearchMoviesService()
        {
            var service = factory.CreateSearchMoviesService();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(TMDbMovieServices.SearchMoviesService));
        }
    }
}
