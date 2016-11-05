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
using Android.Util;

namespace MoviesApp.Xamarin.Droid.Utils
{
    static class FragmentInteraction
    {
        public interface IInteractionListener
        {
            void OnInteraction(string action, Bundle args);
        }

        public static void NotifyInteraction(this Fragment fragment,
            string action, Bundle args)
        {
            var interactionListener = fragment.Activity as IInteractionListener;

            if (interactionListener != null)
                interactionListener.OnInteraction(action, args);
        }

        public static void ExecuteInteraction(this Activity activity,
            int frameContent, Type fragmentType, Bundle args, bool addToBackstack = true)
        {
            var fragmentClass = Java.Lang.Class.FromType(fragmentType);

            if (fragmentClass == null)
                return;

            var fName = fragmentClass.Name;
            var fragment = activity.FragmentManager.FindFragmentByTag(fName);

            if (fragment == null)
            {
                try
                {
                    fragment = Fragment.Instantiate(activity, fName, args);
                }
                catch (Fragment.InstantiationException ex)
                {
                    Log.Error("changeFragment", ex.ToString());
                }
            }

            if (fragment != null)
            {
                var contentFragment = activity.FragmentManager.FindFragmentById(frameContent);
                var ft = activity.FragmentManager.BeginTransaction();

                if (contentFragment == null)
                {
                    ft.Add(frameContent, fragment, fName);
                }
                else
                {
                    ft.Replace(frameContent, fragment, fName);
                }

                if(addToBackstack)
                    ft.AddToBackStack(null);

                ft.Commit();
            }
        }
    }
}
