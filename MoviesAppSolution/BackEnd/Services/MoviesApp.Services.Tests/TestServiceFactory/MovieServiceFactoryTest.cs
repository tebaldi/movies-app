using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesApp.Services.ServiceFactory;
using Moq;
using MoviesApp.Domain.Model;

namespace MoviesApp.Services.Tests.TestServiceFactory
{
    [TestClass]
    public class MovieServiceFactoryTest
    {
        [TestMethod]
        public void ShouldCreateGetMoviesService()
        {
            var movie1 = new Mock<IMovie>();
            movie1.Setup(m => m.MovieID).Returns(1);

            var movie2 = new Mock<IMovie>();
            movie2.Setup(m => m.MovieID).Returns(2);

            var pagedResult = new PagedResult<IMovie>();
            pagedResult.PageIndex = 1;
            pagedResult.TotalPages = 50;
            pagedResult.TotalResults = 100;
            pagedResult.Results = new[] { movie1.Object, movie2.Object };

            var getAllMoviesResponse = new Mock<IServiceResponse<PagedResult<IMovie>>>();
            getAllMoviesResponse.Setup(r => r.ResponseKey)
                .Returns(Guid.Parse("95f17528-8db9-4683-83e8-0daacc4fe71a"));
            getAllMoviesResponse.Setup(r => r.Data)
                .Returns(pagedResult);

            var getAllMoviesService = new Mock<IService<IMovieSearch, PagedResult<IMovie>>>();
            getAllMoviesService.Setup(s => s.ExecuteService(It.IsAny<IServiceRequest<IMovieSearch>>()))
                .Returns(getAllMoviesResponse.Object);

            var factory = new Mock<IMovieServiceFactory>();
            factory.Setup(f => f.CreateGetMoviesService())
                .Returns(getAllMoviesService.Object);

            var movieService = factory.Object.CreateGetMoviesService();
            Assert.IsNotNull(movieService);

            var movieSearch = new Mock<IMovieSearch>();
            movieSearch.Setup(p => p.MovieName).Returns("query_name");
            movieSearch.Setup(p => p.Page).Returns(1);

            var request = new Mock<IServiceRequest<IMovieSearch>>();
            request.Setup(r => r.Data).Returns(movieSearch.Object);

            var response = movieService.ExecuteService(request.Object);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Data);

            Assert.AreEqual("95f17528-8db9-4683-83e8-0daacc4fe71a", response.ResponseKey.ToString());
            Assert.AreEqual(2, response.Data.Results.Length);
            Assert.AreEqual(1, response.Data.Results[0].MovieID);
            Assert.AreEqual(2, response.Data.Results[1].MovieID);
        }

        [TestMethod]
        public void ShouldCreateGetMovieDetailsService()
        {
            var movie = new Mock<IMovieDetails>();
            movie.Setup(m => m.MovieID).Returns(1);
            movie.Setup(m => m.MovieName).Returns("MovieName");
            movie.Setup(m => m.Genre).Returns("Genre");
            movie.Setup(m => m.ImagePath).Returns("PosterImage");
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
            Assert.AreEqual("PosterImage", response.Data.ImagePath);
            Assert.AreEqual("OverView", response.Data.OverView);
            Assert.AreEqual(DateTime.Today, response.Data.ReleaseDate);
        }
    }
}
