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
        public void ShouldCreateGetMoviesService()
        {
            var movie1 = new Movie
            {
                MovieID = 1
            };

            var movie2 = new Movie
            {
                MovieID = 2
            };

            var pagedResult = new PagedResult<Movie>
            {
                PageIndex = 1,
                TotalPages = 50,
                TotalResults = 100,
                Results = new[] { movie1, movie2 }
            };

            var getAllMoviesResponse = new Mock<IServiceResponse<PagedResult<Movie>>>();
            getAllMoviesResponse.Setup(r => r.ResponseKey)
                .Returns(Guid.Parse("95f17528-8db9-4683-83e8-0daacc4fe71a"));
            getAllMoviesResponse.Setup(r => r.Data)
                .Returns(pagedResult);

            var getAllMoviesService = new Mock<IService<MovieSearch, PagedResult<Movie>>>();
            getAllMoviesService.Setup(s => 
                s.ExecuteService(It.IsAny<IServiceRequest<MovieSearch>>()))
                .Returns(getAllMoviesResponse.Object);

            var factory = new Mock<IMovieServiceFactory>();
            factory.Setup(f => f.CreateGetMoviesService())
                .Returns(getAllMoviesService.Object);

            var movieService = factory.Object.CreateGetMoviesService();
            Assert.IsNotNull(movieService);

            var movieSearch = new MovieSearch
            {
                MovieName = "query_name",
                Page = 1
            };

            var request = new Mock<IServiceRequest<MovieSearch>>();
            request.Setup(r => r.Data).Returns(movieSearch);

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
            var movieKey = new MovieKey
            {
                MovieID = 1
            };

            var movie = new MovieDetails
            {
                MovieID = movieKey.MovieID,
                MovieName = "MovieName",
                Genre = "Genre",
                ImagePath = "PosterImage",
                ReleaseDate = DateTime.Today,
                OverView = "OverView"
            };

            var getMovieDetailsRequest = new Mock<IServiceRequest<MovieKey>>();
            getMovieDetailsRequest.Setup(r => r.RequestKey)
                .Returns(Guid.Parse("95f17528-8db9-4683-83e8-0daacc4fe71a"));
            getMovieDetailsRequest.Setup(r => r.Data)
                .Returns(movieKey);

            var getMovieDetailsResponse = new Mock<IServiceResponse<MovieDetails>>();
            getMovieDetailsResponse.Setup(r => r.ResponseKey)
                .Returns(Guid.Parse("95f17528-8db9-4683-83e8-0daacc4fe71a"));
            getMovieDetailsResponse.Setup(r => r.Data)
                .Returns(movie);

            var getMovieDetailsService = new Mock<IService<MovieKey, MovieDetails>>();
            getMovieDetailsService
                .Setup(s => s.ExecuteService(It.IsAny<IServiceRequest<MovieKey>>()))
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
