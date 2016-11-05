using MoviesApp.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.ServiceFactory
{
    public interface IMovieServiceFactory : IServiceFactory
    {
        IService<MovieSearch, PagedResult<Movie>> CreateGetMoviesService();

        IService<MovieKey, MovieDetails> CreateGetMovieDetailsService();
    }
}