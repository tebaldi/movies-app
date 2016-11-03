using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesApp.Infrastructure.TMDb;
using MoviesApp.Services.ServiceFactory;

namespace MoviesApp.Infrastructure.Tests.TMDBTests
{
    [TestClass]
    public class TMDbMovieServiceFactoryTest
    {
        public readonly IMovieServiceFactory factory;

        public TMDbMovieServiceFactoryTest()
        {
            factory = new TMDbMovieServiceFactory();
        }

        [TestMethod]
        public void ShouldCreateGetMoviesService()
        {
            var service = factory.CreateGetMoviesService();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(TMDbMovieServices.GetMoviesService));
        }        

        [TestMethod]
        public void ShouldCreateGetMovieDetailsService()
        {
            var service = factory.CreateGetMovieDetailsService();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(TMDbMovieServices.GetMovieDetailsService));
        }
    }
}
