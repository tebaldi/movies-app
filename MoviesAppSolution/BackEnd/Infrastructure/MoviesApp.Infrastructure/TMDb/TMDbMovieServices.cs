using MoviesApp.Domain.Model;
using MoviesApp.Services;
using MoviesApp.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Infrastructure.TMDb
{
    public class TMDbMovieServices
    {
        public class GetAllMoviesService : IService<IPaginable, PagedResult<IMovie>>
        {

            IServiceResponse<PagedResult<IMovie>> IService<IPaginable, PagedResult<IMovie>>
                .ExecuteService(IServiceRequest<IPaginable> request)
            {
                var page = request.Data.Page;
                var uri = TMDbApi.UpcomingMovies.CreateUri(page);
                var response = TMDbApi.MakeApiRequest(uri);

                var results = response["results"] as JArray;
                var movies = results.Select(result => new TMDbMovie
                {
                    MovieID = int.Parse(result["id"].ToString()),
                    MovieName = result["title"].ToString(),
                    //Genre = result["genre_ids"],
                    PosterImage = result["poster_path"].ToString(),
                    ReleaseDate = DateTime.Parse(result["release_date"].ToString()),
                } as IMovie).ToArray();

                var pagedResult = new PagedResult<IMovie>();
                pagedResult.PageIndex = int.Parse(response["page"].ToString());
                pagedResult.TotalPages = int.Parse(response["total_pages"].ToString());
                pagedResult.TotalResults = int.Parse(response["total_results"].ToString());
                pagedResult.Results = movies;

                var serviceResponse = new ServiceResponse<PagedResult<IMovie>>
                {
                    ResponseKey = Guid.NewGuid(),
                    Data = pagedResult
                };

                return serviceResponse;
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
