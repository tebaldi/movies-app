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

            var movies = getAllMoviesService.ExecuteService(request.Object);
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
        public void ShouldCreateSearchMoviesService()
        {
            //var paginable = new Mock<IMovieKey>();
            //paginable.Setup(p => p.Page).Returns(1);

            //var request = new Mock<IServiceRequest<IMovieKey>>();
            //request.Setup(r => r.Data).Returns(paginable.Object);

            //var movies = searchMoviesService.ExecuteService(request.Object);
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ShouldCreateGetMovieDetailsService()
        {
            Assert.Inconclusive();
        }
    }
}
