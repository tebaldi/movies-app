using MoviesApp.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.ServiceFactory
{
    public interface IMovieServiceFactory
    {
        IService<PagedResult<IMovie>> CreateGetAllMoviesService();

        IService<PagedResult<IMovie>, IMovieKey> CreateSearchMoviesService();

        IService<IMovieDetails, IMovieKey> CreateGetMovieDetailsService();
    }
}