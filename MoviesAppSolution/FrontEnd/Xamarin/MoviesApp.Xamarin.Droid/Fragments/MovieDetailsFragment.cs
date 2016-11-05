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

namespace MoviesApp.Xamarin.Droid.Fragments
{
    public class MovieDetailsFragment : FragmentBase
    {
        public const string MovieId = "MovieId";

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.movie_details_fragment, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var movieId = Arguments != null && Arguments.ContainsKey(MovieId)
                ? Arguments.GetInt(MovieId) : 0;

            LoadDetails(movieId);
        }

        private void LoadDetails(int movieId)
        {

        }
    }
}