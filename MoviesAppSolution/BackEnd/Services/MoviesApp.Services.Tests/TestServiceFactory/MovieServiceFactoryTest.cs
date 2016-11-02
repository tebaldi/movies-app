using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesApp.Services.ServiceFactory;
using Moq;
using MoviesApp.Services.Dto;
using MoviesApp.Services.Tests.TestDto;

namespace MoviesApp.Services.Tests.TestServiceFactory
{
    [TestClass]
    public class MovieServiceFactoryTest
    {
        private IMovieServiceFactory movieFactory;

        [TestInitialize]
        public void Setup()
        {
            var responseKey = Guid.Parse("95f17528-8db9-4683-83e8-0daacc4fe71a");
            var requestKey = Guid.Parse("d59cd40f-b8b9-4616-820f-78abda321d68");
            var movie1 = new Mock<IMovie
            {
                MovieID = 1,
                Genre = "genre1",
                MovieName = "name1",
                OverView = "overview1",
                PosterImage = "image1",
                ReleaseDate = DateTime.Today
            };
            var movie2 = new MovieTestDto
            {
                MovieID = 2,
                Genre = "genre2",
                MovieName = "name2",
                OverView = "overview2",
                PosterImage = "image2",
                ReleaseDate = DateTime.Today
            };

            var pagedResult = new PagedResult<IMovie>();
            pagedResult.PageIndex = 1;
            pagedResult.PageSize = 50;
            pagedResult.TotalRecord = 100;
            pagedResult.Result = new[] { movie1, movie2 };

            var serviceResponse = new Mock<IServiceResponse<PagedResult<IMovie>>>();
            serviceResponse.Setup(r => r.ResponseKey)
                .Returns(responseKey);
            serviceResponse.Setup(r => r.Data)
                .Returns(pagedResult);

            var service = new Mock<IService<PagedResult<IMovie>>>();
            service.Setup(s => s.ExecuteService())
                .Returns(serviceResponse.Object);

            var factory = new Mock<IMovieServiceFactory>();
            factory.Setup(f => f.CreateGetAllMoviesService())
                .Returns(service.Object);

            this.movieFactory = factory.Object;
        }

        [TestMethod]
        public void ShouldCreateGetAllMoviesService()
        {
            var service = movieFactory.CreateGetAllMoviesService();

            Assert.IsNotNull(service);

            var response = service.ExecuteService();
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
        }

        [TestMethod]
        public void ShouldCreateGetMovieDetailsService()
        {
        }
    }
}
