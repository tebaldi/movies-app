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
using MoviesApp.Services.ServiceFactory;
using MoviesApp.Infrastructure.MobileData.Tasks;
using MoviesApp.Services;

namespace MoviesApp.Xamarin.Droid.Adapters
{
    class MoviesAdapter : BaseAdapter<Movie>
    {
        private readonly Activity context;
        private readonly IMovieServiceFactory movieServiceFactory;
        private readonly List<Movie> movies = new List<Movie>();
        private MovieSearch lastSearch;
        private PagedResult<Movie> lastResult;
        private bool isLoading;

        public MoviesAdapter(Activity context,
            IMovieServiceFactory movieServiceFactory)
        {
            this.context = context;
            this.movieServiceFactory = movieServiceFactory;
        }

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

            if (lastResult?.PageIndex > lastResult?.TotalPages)
                return;

            LoadMoviesInBackground(search);
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

        private void LoadMoviesInBackground(string search)
        {
            var task = AsyncTaskExecutor<MovieSearch, PagedResult<Movie>>.Builder
                .SetOnPreExecuteAction(() =>
                {
                    if (context == null || context.Handle == IntPtr.Zero)
                        return;

                    isLoading = true;
                    context.SetProgressBarIndeterminateVisibility(isLoading);
                })
                .SetOnBackgroundAction(args =>
                {
                    var service = movieServiceFactory.CreateGetMoviesService();
                    var response = service.ExecuteService(new ServiceRequest<MovieSearch>
                    {
                        RequestKey = Guid.NewGuid(),
                        Data = args.First()
                    });
                    return response.Data;
                })
                .SetOnPostExecuteAction(result =>
                {
                    if (context == null || context.Handle == IntPtr.Zero)
                        return;

                    isLoading = false;
                    context.SetProgressBarIndeterminateVisibility(isLoading);

                    if (result == null)
                        return;

                    movies.AddRange(result.Results);
                    lastResult = result;
                    NotifyDataSetChanged();
                })
                .Build();

            if (task != null)
            {
                lastSearch = new MovieSearch
                {
                    MovieName = search,
                    Page = (lastSearch?.Page ?? 0) + 1
                };

                task.Execute(new[] { lastSearch });
            }
        }
    }
}