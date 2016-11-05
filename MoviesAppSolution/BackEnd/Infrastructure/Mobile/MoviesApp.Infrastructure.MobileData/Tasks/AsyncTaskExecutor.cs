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
using MoviesApp.Infrastructure.MobileData.Network;
using Android.Util;

namespace MoviesApp.Infrastructure.MobileData.Tasks
{
    public class AsyncTaskExecutor :
        AsyncTaskExecutor<object, object>
    {
        protected AsyncTaskExecutor(TaskBuilder builder) : base(builder) { }
    }

    public class AsyncTaskExecutor<TResult> :
        AsyncTaskExecutor<object, TResult>
    {
        protected AsyncTaskExecutor(TaskBuilder builder) : base(builder) { }
    }

    public class AsyncTaskExecutor<TParams, TResult> :
        AsyncTask<TParams, object, TResult>
    {
        private readonly TaskBuilder builder;

        protected AsyncTaskExecutor(TaskBuilder builder)
        {
            this.builder = builder;
        }

        protected override void OnPreExecute()
        {
            base.OnPreExecute();

            builder.onPreExecuteAction?.Invoke();
        }

        protected override TResult RunInBackground(params TParams[] @params)
        {
            if (IsCancelled)
                return default(TResult);
            try
            {
                return builder.onBackgroundAction.Invoke(@params);
            }
            catch(Exception e)
            {
                Log.Error("RunInBackground", e.ToString());

                return default(TResult);
            }
        }

        protected override void OnCancelled()
        {
            base.OnCancelled();

            builder.onCancelledAction?.Invoke();
        }

        protected override void OnPostExecute(TResult result)
        {
            if (!NetworkConnector.HasConnection())
            {
                Toast.MakeText(Application.Context,
                    "Please verify your network connection...", ToastLength.Short)
                    .Show();
            }

            builder.onPostExecuteAction?.Invoke(result);
        }

        public static TaskBuilder Builder { get { return new TaskBuilder(); } }

        public class TaskBuilder
        {
            internal Action onPreExecuteAction;
            internal Func<TParams[], TResult> onBackgroundAction;
            internal Action<TResult> onPostExecuteAction;
            internal Action onCancelledAction;

            public TaskBuilder SetOnPreExecuteAction(Action action)
            {
                onPreExecuteAction = action;
                return this;
            }

            public TaskBuilder SetOnBackgroundAction(Func<TParams[], TResult> action)
            {
                onBackgroundAction = action;
                return this;
            }

            public TaskBuilder SetOnPostExecuteAction(Action<TResult> action)
            {
                onPostExecuteAction = action;
                return this;
            }

            public TaskBuilder SetOnCancelledAction(Action action)
            {
                onCancelledAction = action;
                return this;
            }

            public AsyncTaskExecutor<TParams, TResult> Build()
            {
                return new AsyncTaskExecutor<TParams, TResult>(this);
            }
        }
    }
}