using MoviesApp.Domain.Model;
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
        IService<IPaginable, PagedResult<IMovie>> CreateGetAllMoviesService();

        IService<IMovieKey, PagedResult<IMovie>> CreateSearchMoviesService();

        IService<IMovieKey, IMovieDetails> CreateGetMovieDetailsService();
    }
}