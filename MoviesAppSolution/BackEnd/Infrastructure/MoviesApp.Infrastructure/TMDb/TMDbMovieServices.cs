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
        public class GetMoviesService : IService<IMovieSearch, PagedResult<IMovie>>
        {
            IServiceResponse<PagedResult<IMovie>> IService<IMovieSearch, PagedResult<IMovie>>
                .ExecuteService(IServiceRequest<IMovieSearch> request)
            {
                var uri = String.IsNullOrEmpty(request.Data.MovieName)
                    ? TMDbApi.UpcomingMovies.CreateUri(request.Data.Page)
                    : TMDbApi.SearchMovie.CreateUri(request.Data.MovieName, request.Data.Page);
                                
                var response = TMDbApi.MakeApiRequest(uri);
                
                var results = response["results"] as JArray;

                var movies = results.Select(result => 
                    new TMDbMovie(result) as IMovie).ToArray();

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
                var uri = TMDbApi.MovieDetails.CreateUri(request.Data.MovieID);
                var response = TMDbApi.MakeApiRequest(uri);

                var movie = new TMDbMovie(response);

                var serviceResponse = new ServiceResponse<IMovieDetails>
                {
                    ResponseKey = Guid.NewGuid(),
                    Data = movie
                };

                return serviceResponse;
            }
        }
    }
}
