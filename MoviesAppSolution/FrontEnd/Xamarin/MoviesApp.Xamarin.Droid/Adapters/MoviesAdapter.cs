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
using Com.Squareup.Picasso;
using Android.Util;

namespace MoviesApp.Xamarin.Droid.Adapters
{
    class MoviesAdapter : BaseAdapter<Movie>
    {
        private readonly List<Movie> movies = new List<Movie>();

        public MoviesAdapter()
        {
            movies.Add(new Movie { MovieID = 1, MovieName = "Movie 1", Genre = "Drama", ImagePath = "https://a2ua.com/poster/poster-006.jpg" });
            movies.Add(new Movie { MovieID = 2, MovieName = "Movie 2", Genre = "Action" });
            movies.Add(new Movie { MovieID = 3, MovieName = "Movie 3", Genre = "New", ReleaseDate = DateTime.Today });
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

                LoadHeader(
                    convertView.FindViewById<TextView>(Resource.Id.header), movie);

                LoadContent(
                    convertView.FindViewById<TextView>(Resource.Id.content), movie);

                LoadImage(
                    convertView.FindViewById<ImageView>(Resource.Id.img), movie);
            }

            return convertView;
        }

        private void LoadHeader(TextView header, Movie movie)
        {
            header.Text = movie.MovieName;
        }

        private void LoadContent(TextView content, Movie movie)
        {
            if (String.IsNullOrEmpty(movie.Genre))
                content.Text = $"Genre: {movie.Genre}/n";

            if (!DateTime.MinValue.Equals(movie.ReleaseDate))
                content.Text = $"Release: {movie.ReleaseDate}";
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