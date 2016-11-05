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

namespace MoviesApp.Xamarin.Droid.Fragments
{
    public class FragmentBase : Fragment
    {
        public override void OnResume()
        {
            base.OnResume();

            SetHasOptionsMenu(true);

            Activity.InvalidateOptionsMenu();
        }
    }
}