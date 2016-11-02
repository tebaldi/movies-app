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
        public readonly IService<IPaginable, PagedResult<IMovie>> getAllMoviesService;
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
            var paginable = new Mock<IPaginable>();
            paginable.Setup(p => p.Page).Returns(1);

            var request = new Mock<IServiceRequest<IPaginable>>();
            request.Setup(r => r.Data).Returns(paginable.Object);

            var allMovies = getAllMoviesService.ExecuteService(request.Object);
            Assert.IsNotNull(allMovies);
            Assert.IsNotNull(allMovies.Data);
            Assert.IsNotNull(allMovies.Data.Results);
            Assert.AreNotEqual(Guid.Empty, allMovies.ResponseKey);
            Assert.AreNotEqual(0, allMovies.Data.PageIndex);
            Assert.AreNotEqual(0, allMovies.Data.TotalResults);
            Assert.AreNotEqual(0, allMovies.Data.TotalPages);
            Assert.IsTrue(allMovies.Data.Results.Length > 0);
        }

        [TestMethod]
        public void ShouldCreateGetMovieDetailsService()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ShouldCreateSearchMoviesService()
        {
            Assert.Inconclusive();
        }
    }
}
