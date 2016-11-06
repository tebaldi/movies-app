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
using MoviesApp.Xamarin.Droid.Utils;
using Android.Util;
using Square.Picasso;
using MoviesApp.Services.Dto;
using MoviesApp.Infrastructure.MobileData.Tasks;
using MoviesApp.Services.ServiceFactory;
using MoviesApp.Infrastructure.MobileData;
using MoviesApp.Services;

namespace MoviesApp.Xamarin.Droid.Fragments
{
    public class MovieDetailsFragment : BaseFragment
    {
        public const string MovieId = "MovieId";

        private IMovieServiceFactory movieServiceFactory;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.movie_details_fragment, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            movieServiceFactory = App.ResolveFactory<IMovieServiceFactory>();

            var movieId = Arguments != null && Arguments.ContainsKey(MovieId)
                ? Arguments.GetInt(MovieId) : 0;

            LoadDetailsInBackground(movieId);
        }

        private void LoadHeader(TextView header, MovieDetails movieDetails)
        {
            var textBuilder = new StringBuilder();

            textBuilder.Append(movieDetails.MovieName);

            header.TextFormatted = Android.Text.Html.FromHtml(textBuilder.ToString());
        }

        private void LoadContent(TextView content, MovieDetails movieDetails)
        {
            var textBuilder = new StringBuilder();

            if (!String.IsNullOrEmpty(movieDetails.Genre))
                textBuilder.Append($"Genre: {movieDetails.Genre}<br/>");

            if (!String.IsNullOrEmpty(movieDetails.OverView))
                textBuilder.Append($"OverView: {movieDetails.OverView}<br/>");

            if (!DateTime.MinValue.Equals(movieDetails.ReleaseDate))
                textBuilder.Append($"Release: {movieDetails.ReleaseDate}");

            content.TextFormatted = Android.Text.Html.FromHtml(textBuilder.ToString());
        }

        private void LoadImage(ImageView imageView, MovieDetails movieDetails)
        {
            var hasImage = !String.IsNullOrEmpty(movieDetails.ImagePath);

            if (hasImage)
            {
                try
                {
                    Picasso.With(imageView.Context)
                        .Load(movieDetails.ImagePath)
                        .Placeholder(Resource.Drawable.Icon)
                        .Error(Resource.Drawable.Icon)
                        .Into(imageView);
                }
                catch (Exception e)
                {
                    Log.Error($"{movieDetails.MovieID} - Picasso", e.ToString());
                    hasImage = false;
                }
            }

            if (!hasImage)
                imageView.SetImageResource(Resource.Drawable.Icon);
        }

        private void LoadDetailsInBackground(int movieId)
        {
            var task = AsyncTaskExecutor<MovieKey, MovieDetails>.Builder
                .SetOnPreExecuteAction(() =>
                {
                    if (Activity == null || Activity.Handle == IntPtr.Zero)
                        return;

                    Activity.SetProgressBarIndeterminateVisibility(true);
                })
                .SetOnBackgroundAction(args =>
                {
                    var service = movieServiceFactory.CreateGetMovieDetailsService();
                    var response = service.ExecuteService(new ServiceRequest<MovieKey>
                    {
                        RequestKey = Guid.NewGuid(),
                        Data = args.First()
                    });
                    return response.Data;
                })
                .SetOnPostExecuteAction(result =>
                {
                    if (Activity == null || Activity.Handle == IntPtr.Zero)
                        return;

                    Activity.SetProgressBarIndeterminateVisibility(false);

                    if (result == null)
                        return;

                    LoadHeader(View.FindViewById<TextView>(Resource.Id.header), result);
                    LoadContent(View.FindViewById<TextView>(Resource.Id.content), result);
                    LoadImage(View.FindViewById<ImageView>(Resource.Id.img), result);
                })
                .Build();

            if(task != null)
                task.Execute(new[] { new MovieKey { MovieID = movieId } });
        }
    }
}