using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using MoviesApp.Xamarin.Droid.Utils;
using MoviesApp.Xamarin.Droid.Adapters;

namespace MoviesApp.Xamarin.Droid.Fragments
{
    public class MoviesFragment : BaseFragment
    {
        public const string MovieWasSelectedAction = "MovieWasSelectedAction";
        public const string MovieWasSelectedAction_MovieId = "MovieId";

        private CancellationTokenSource queryTokenSource;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.movies_fragment, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var list = View.FindViewById<ListView>(Android.Resource.Id.List);
            list.ItemClick -= List_ItemClick;
            list.ItemClick += List_ItemClick;

            list.Adapter = new MoviesAdapter();
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var movieId = e.Id;

            var args = new Bundle();
            args.PutInt(MovieWasSelectedAction_MovieId, (int)movieId);

            this.NotifyInteraction(MovieWasSelectedAction, args);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);

            inflater.Inflate(Resource.Menu.movies_fragment_menu, menu);

            var actionView = menu.FindItem(Resource.Id.menuSearch).ActionView;
            var menuSearch = actionView.JavaCast<SearchView>();

            menuSearch.QueryTextChange -= MenuSearch_QueryTextChange;
            menuSearch.QueryTextChange += MenuSearch_QueryTextChange;
        }

        private void MenuSearch_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            if (queryTokenSource != null)
                queryTokenSource.Cancel();

            queryTokenSource = new CancellationTokenSource();

            new Timer((token) =>
            {
                if (((CancellationToken)token).IsCancellationRequested)
                    return;

                Activity.RunOnUiThread(delegate
                {
                    Toast.MakeText(Activity, e.NewText, ToastLength.Short).Show();
                });

            }, queryTokenSource.Token, 1500, 0);
        }
    }
}