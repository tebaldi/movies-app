using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesApp.Services.ServiceFactory;
using Moq;
using MoviesApp.Services.Dto;

namespace MoviesApp.Services.Tests.TestServiceFactory
{
    [TestClass]
    public class MovieServiceFactoryTest
    {
        [TestMethod]
        public void ShouldCreateGetAllMoviesService()
        {
            var movie1 = new Mock<IMovie>();
            movie1.Setup(m => m.MovieID).Returns(1);

            var movie2 = new Mock<IMovie>();
            movie2.Setup(m => m.MovieID).Returns(2);

            var pagedResult = new PagedResult<IMovie>();
            pagedResult.PageIndex = 1;
            pagedResult.PageSize = 50;
            pagedResult.TotalRecord = 100;
            pagedResult.Result = new[] { movie1.Object, movie2.Object };

            var getAllMoviesResponse = new Mock<IServiceResponse<PagedResult<IMovie>>>();
            getAllMoviesResponse.Setup(r => r.ResponseKey)
                .Returns(Guid.Parse("95f17528-8db9-4683-83e8-0daacc4fe71a"));
            getAllMoviesResponse.Setup(r => r.Data)
                .Returns(pagedResult);

            var getAllMoviesService = new Mock<IService<PagedResult<IMovie>>>();
            getAllMoviesService.Setup(s => s.ExecuteService())
                .Returns(getAllMoviesResponse.Object);

            var factory = new Mock<IMovieServiceFactory>();
            factory.Setup(f => f.CreateGetAllMoviesService())
                .Returns(getAllMoviesService.Object);

            var movieService = factory.Object.CreateGetAllMoviesService();
            Assert.IsNotNull(movieService);

            var response = movieService.ExecuteService();
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Data);

            Assert.AreEqual("95f17528-8db9-4683-83e8-0daacc4fe71a", response.ResponseKey.ToString());
            Assert.AreEqual(2, response.Data.Result.Length);
            Assert.AreEqual(1, response.Data.Result[0].MovieID);
            Assert.AreEqual(2, response.Data.Result[1].MovieID);
        }

        [TestMethod]
        public void ShouldCreateSearchMoviesService()
        {
            var movie1 = new Mock<IMovie>();
            movie1.Setup(m => m.MovieID).Returns(1);

            var pagedResult = new PagedResult<IMovie>();
            pagedResult.PageIndex = 1;
            pagedResult.PageSize = 50;
            pagedResult.TotalRecord = 1;
            pagedResult.Result = new[] { movie1.Object };

            var searchMovieRequest = new Mock<IServiceRequest<IMovieKey>>();
            searchMovieRequest.Setup(r => r.RequestKey)
                .Returns(Guid.Parse("95f17528-8db9-4683-83e8-0daacc4fe71a"));
            searchMovieRequest.Setup(r => r.Data)
                .Returns(movie1.Object);

            var searchMovieResponse = new Mock<IServiceResponse<PagedResult<IMovie>>>();
            searchMovieResponse.Setup(r => r.ResponseKey)
                .Returns(Guid.Parse("95f17528-8db9-4683-83e8-0daacc4fe71a"));
            searchMovieResponse.Setup(r => r.Data)
                .Returns(pagedResult);

            var searchMoviesService = new Mock<IService<IMovieKey, PagedResult<IMovie>>>();
            searchMoviesService
                .Setup(s => s.ExecuteService(It.IsAny<IServiceRequest<IMovieKey>>()))
                .Returns(searchMovieResponse.Object);

            var factory = new Mock<IMovieServiceFactory>();            
            factory.Setup(f => f.CreateSearchMoviesService())
                .Returns(searchMoviesService.Object);

            var movieService = factory.Object.CreateSearchMoviesService();
            Assert.IsNotNull(movieService);

            var response = movieService.ExecuteService(searchMovieRequest.Object);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Data);

            Assert.AreEqual("95f17528-8db9-4683-83e8-0daacc4fe71a", response.ResponseKey.ToString());
            Assert.AreEqual(1, response.Data.Result.Length);
            Assert.AreEqual(1, response.Data.Result[0].MovieID);
        }

        [TestMethod]
        public void ShouldCreateGetMovieDetailsService()
        {
            var movie = new Mock<IMovieDetails>();
            movie.Setup(m => m.MovieID).Returns(1);
            movie.Setup(m => m.MovieName).Returns("MovieName");
            movie.Setup(m => m.Genre).Returns("Genre");
            movie.Setup(m => m.PosterImage).Returns("PosterImage");
            movie.Setup(m => m.ReleaseDate).Returns(DateTime.Today);
            movie.Setup(m => m.OverView).Returns("OverView");

            var getMovieDetailsRequest = new Mock<IServiceRequest<IMovieKey>>();
            getMovieDetailsRequest.Setup(r => r.RequestKey)
                .Returns(Guid.Parse("95f17528-8db9-4683-83e8-0daacc4fe71a"));
            getMovieDetailsRequest.Setup(r => r.Data)
                .Returns(movie.Object);

            var getMovieDetailsResponse = new Mock<IServiceResponse<IMovieDetails>>();
            getMovieDetailsResponse.Setup(r => r.ResponseKey)
                .Returns(Guid.Parse("95f17528-8db9-4683-83e8-0daacc4fe71a"));
            getMovieDetailsResponse.Setup(r => r.Data)
                .Returns(movie.Object);

            var getMovieDetailsService = new Mock<IService<IMovieKey, IMovieDetails>>();
            getMovieDetailsService
                .Setup(s => s.ExecuteService(It.IsAny<IServiceRequest<IMovieKey>>()))
                .Returns(getMovieDetailsResponse.Object);

            var factory = new Mock<IMovieServiceFactory>();
            factory.Setup(f => f.CreateGetMovieDetailsService())
                .Returns(getMovieDetailsService.Object);

            var movieService = factory.Object.CreateGetMovieDetailsService();
            Assert.IsNotNull(movieService);

            var response = movieService.ExecuteService(getMovieDetailsRequest.Object);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Data);

            Assert.AreEqual("95f17528-8db9-4683-83e8-0daacc4fe71a", response.ResponseKey.ToString());
            Assert.AreEqual(1, response.Data.MovieID);
            Assert.AreEqual("MovieName", response.Data.MovieName);
            Assert.AreEqual("Genre", response.Data.Genre);
            Assert.AreEqual("PosterImage", response.Data.PosterImage);
            Assert.AreEqual("OverView", response.Data.OverView);
            Assert.AreEqual(DateTime.Today, response.Data.ReleaseDate);
        }
    }
}
