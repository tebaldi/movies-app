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
        IService<IMovieSearch, PagedResult<IMovie>> CreateGetMoviesService();

        IService<IMovieKey, IMovieDetails> CreateGetMovieDetailsService();
    }
}