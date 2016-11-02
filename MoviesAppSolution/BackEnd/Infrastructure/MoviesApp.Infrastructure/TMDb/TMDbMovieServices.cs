using MoviesApp.Services;
using MoviesApp.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Infrastructure.TMDb
{
    public class TMDbMovieServices
    {
        public class GetAllMoviesService : IService<PagedResult<IMovie>>
        {
            IServiceResponse<PagedResult<IMovie>> IService<PagedResult<IMovie>>
                .ExecuteService()
            {
                var uri = TMDbApi.UpcomingMovies.CreateUri(1);
                var json = TMDbApi.MakeApiRequest(uri);

                var pagedResult = new PagedResult<IMovie>();
                pagedResult.PageIndex;
                pagedResult.PageSize;
                pagedResult.TotalRecords;
                pagedResult.Result;

                var response = new ServiceResponse<PagedResult<IMovie>>
                {
                    ResponseKey = Guid.NewGuid(),
                    Data = ;
                };

                return response;
            }
        }

        public class GetMovieDetailsService : IService<IMovieKey, IMovieDetails>
        {
            IServiceResponse<IMovieDetails> IService<IMovieKey, IMovieDetails>
                .ExecuteService(IServiceRequest<IMovieKey> request)
            {
                throw new NotImplementedException();
            }
        }

        public class SearchMoviesService : IService<IMovieKey, PagedResult<IMovie>>
        {
            IServiceResponse<PagedResult<IMovie>> IService<IMovieKey, PagedResult<IMovie>>
                .ExecuteService(IServiceRequest<IMovieKey> request)
            {
                throw new NotImplementedException();
            }
        }
    }
}
