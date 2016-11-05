using MoviesApp.Services.Dto;
using MoviesApp.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Infrastructure.TMDb
{
    internal class TMDbMovieServices
    {
        public class GetMoviesService : IService<MovieSearch, PagedResult<Movie>>
        {
            IServiceResponse<PagedResult<Movie>> IService<MovieSearch, PagedResult<Movie>>
                .ExecuteService(IServiceRequest<MovieSearch> request)
            {
                var uri = request.Data.Upcoming
                    ? TMDbApi.UpcomingMovies.CreateUri(request.Data.Page)
                    : TMDbApi.SearchMovie.CreateUri(request.Data.MovieName, request.Data.Page);
                                
                var response = TMDbApi.MakeApiRequest(uri);
                
                var results = response["results"] as JArray;

                var movies = results.Select(result => 
                    new TMDbMovie(result) as Movie).ToArray();

                var pagedResult = new PagedResult<Movie>();
                pagedResult.PageIndex = int.Parse(response["page"].ToString());
                pagedResult.TotalPages = int.Parse(response["total_pages"].ToString());
                pagedResult.TotalResults = int.Parse(response["total_results"].ToString());
                pagedResult.Results = movies;

                var serviceResponse = new ServiceResponse<PagedResult<Movie>>
                {
                    ResponseKey = Guid.NewGuid(),
                    Data = pagedResult
                };

                return serviceResponse;
            }
        }

        public class GetMovieDetailsService : IService<MovieKey, MovieDetails>
        {
            IServiceResponse<MovieDetails> IService<MovieKey, MovieDetails>
                .ExecuteService(IServiceRequest<MovieKey> request)
            {
                var uri = TMDbApi.MovieDetails.CreateUri(request.Data.MovieID);
                var response = TMDbApi.MakeApiRequest(uri);

                var movie = new TMDbMovie(response);

                var serviceResponse = new ServiceResponse<MovieDetails>
                {
                    ResponseKey = Guid.NewGuid(),
                    Data = movie
                };

                return serviceResponse;
            }
        }
    }
}
