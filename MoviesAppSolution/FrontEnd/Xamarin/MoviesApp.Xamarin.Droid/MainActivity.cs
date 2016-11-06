using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;
using MoviesApp.Xamarin.Droid.Utils;
using MoviesApp.Xamarin.Droid.Fragments;

namespace MoviesApp.Xamarin.Droid
{
    [Activity(Label = "@string/AppName", MainLauncher = true,
        ConfigurationChanges = 
            Android.Content.PM.ConfigChanges.KeyboardHidden |
            Android.Content.PM.ConfigChanges.Orientation |
            Android.Content.PM.ConfigChanges.ScreenSize,
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Sensor)]
    [IntentFilter(new[] { Intent.ActionSearch })]
    [MetaData(("android.app.searchable"), Resource = "@xml/searchable")]
    public class MainActivity : Activity, FragmentInteraction.IInteractionListener
    {
        protected override void OnCreate(Bundle bundle)
        {
            Window.RequestFeature(WindowFeatures.ActionBar);
            Window.RequestFeature(WindowFeatures.IndeterminateProgress);

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.main_activity);

            ActionBar.SetDisplayHomeAsUpEnabled(true);

            this.ExecuteInteraction(Resource.Id.frameContent,
                typeof(MoviesFragment), new Bundle(), false);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                if (FragmentManager.BackStackEntryCount > 0)
                    FragmentManager.PopBackStack();
                else
                    Finish();
            }

            return base.OnOptionsItemSelected(item);
        }

        void FragmentInteraction.IInteractionListener.OnInteraction(
            string action, Bundle args)
        {
            switch(action)
            {
                case MoviesFragment.MovieWasSelectedAction:
                    {
                        this.ExecuteInteraction(Resource.Id.frameContent,
                            typeof(MovieDetailsFragment), args);
                    }
                    break;
            }
        }
    }
}

