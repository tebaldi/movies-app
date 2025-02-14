﻿using MoviesApp.Services.ServiceFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesApp.Services;
using MoviesApp.Services.Dto;

namespace MoviesApp.Infrastructure.TMDb
{
    internal class TMDbMovieServiceFactory : IMovieServiceFactory
    {
        IService<MovieSearch, PagedResult<Movie>> IMovieServiceFactory
            .CreateGetMoviesService()
        {
            return new TMDbMovieServices.GetMoviesService();
        }

        IService<MovieKey, MovieDetails> IMovieServiceFactory
            .CreateGetMovieDetailsService()
        {
            return new TMDbMovieServices.GetMovieDetailsService();
        }
    }
}
