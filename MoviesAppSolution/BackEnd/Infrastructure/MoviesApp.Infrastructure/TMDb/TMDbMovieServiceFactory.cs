using MoviesApp.Services.ServiceFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesApp.Services;
using MoviesApp.Services.Dto;
using MoviesApp.Domain.Model;

namespace MoviesApp.Infrastructure.TMDb
{
    public class TMDbMovieServiceFactory : IMovieServiceFactory
    {
        IService<IPaginable, PagedResult<IMovie>> IMovieServiceFactory
            .CreateGetAllMoviesService()
        {
            return new TMDbMovieServices.GetAllMoviesService();
        }

        IService<IMovieKey, IMovieDetails> IMovieServiceFactory
            .CreateGetMovieDetailsService()
        {
            return new TMDbMovieServices.GetMovieDetailsService();
        }

        IService<IMovieKey, PagedResult<IMovie>> IMovieServiceFactory
            .CreateSearchMoviesService()
        {
            return new TMDbMovieServices.SearchMoviesService();
        }
    }
}
