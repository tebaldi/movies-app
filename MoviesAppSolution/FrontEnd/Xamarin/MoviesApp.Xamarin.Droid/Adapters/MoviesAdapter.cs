using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MoviesApp.Services.Dto;
using Android.Util;
using Square.Picasso;

namespace MoviesApp.Xamarin.Droid.Adapters
{
    class MoviesAdapter : BaseAdapter<Movie>
    {
        private readonly List<Movie> movies = new List<Movie>();
        private MovieSearch lastSearch;
        private PagedResult<Movie> lastResult;
        private bool isLoading;

        public void LoadMovies(string search)
        {
            if (isLoading)
                return;

            if (lastSearch?.MovieName != search)
            {
                movies.Clear();
                lastResult = default(PagedResult<Movie>);
                lastSearch = default(MovieSearch);
            }

            lastSearch = new MovieSearch
            {
                MovieName = search,
                Page = (lastSearch?.Page ?? 0) + 1
            };

            if (lastResult?.PageIndex > lastResult?.TotalPages)
                return;

            movies.Add(new Movie { MovieID = 1, MovieName = "Movie 1", Genre = "Drama", ImagePath = "https://a2ua.com/poster/poster-006.jpg" });
            movies.Add(new Movie { MovieID = 2, MovieName = "Movie 2", Genre = "Action" });
            movies.Add(new Movie { MovieID = 3, MovieName = "Movie 3", Genre = "New", ReleaseDate = DateTime.Today });

            lastResult = new PagedResult<Movie>()
            {
                PageIndex = lastSearch.Page,
                TotalPages = 3,
                TotalResults = 3 * 3
            };

            NotifyDataSetChanged();
            isLoading = false;
        }

        public override Movie this[int position]
        {
            get
            {
                return movies.Count > position ? movies[position] : default(Movie);
            }
        }

        public override int Count
        {
            get
            {
                return movies.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return movies.Count > position
                ? movies[position].MovieID
                : default(long);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (movies.Count > position)
            {
                var movie = movies[position];

                if (convertView == null)
                {
                    convertView = LayoutInflater.FromContext(Application.Context)
                        .Inflate(Resource.Layout.movie_item, null);
                }

                LoadHeader(convertView.FindViewById<TextView>(Resource.Id.header), movie);
                LoadContent(convertView.FindViewById<TextView>(Resource.Id.content), movie);
                LoadImage(convertView.FindViewById<ImageView>(Resource.Id.img), movie);
            }

            return convertView;
        }

        private void LoadHeader(TextView header, Movie movie)
        {
            var textBuilder = new StringBuilder();

            textBuilder.Append(movie.MovieName);

            header.TextFormatted = Android.Text.Html.FromHtml(textBuilder.ToString());
        }

        private void LoadContent(TextView content, Movie movie)
        {
            var textBuilder = new StringBuilder();

            if (!String.IsNullOrEmpty(movie.Genre))
                textBuilder.Append($"Genre: {movie.Genre}<br/>");

            if (!DateTime.MinValue.Equals(movie.ReleaseDate))
                textBuilder.Append($"Release: {movie.ReleaseDate}");

            content.TextFormatted = Android.Text.Html.FromHtml(textBuilder.ToString());
        }

        private void LoadImage(ImageView imageView, Movie movie)
        {
            var hasImage = !String.IsNullOrEmpty(movie.ImagePath);

            if (hasImage)
            {
                try
                {
                    Picasso.With(imageView.Context)
                        .Load(movie.ImagePath)
                        .Placeholder(Resource.Drawable.Icon)
                        .Error(Resource.Drawable.Icon)
                        .Resize(200, 200)
                        .CenterCrop()
                        .Into(imageView);
                }
                catch (Exception e)
                {
                    Log.Error($"{movie.MovieID} - Picasso", e.ToString());
                    hasImage = false;
                }
            }

            if(!hasImage)
                imageView.SetImageResource(Resource.Drawable.Icon);
        }
    }
}