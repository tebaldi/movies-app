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
using NUnit.Framework;
using MoviesApp.Infrastructure.MobileData.Tasks;
using System.Threading;

namespace MoviesApp.Infrastructure.MobileData.Tests.Tasks
{
    [TestFixture]
    class AsyncTaskExecutorTest
    {
        [Test]
        [Ignore]
        public void ShouldExecutTaskInBackground()
        {
            var calledOnPreExecuteAction = false;
            var calledOnBackgroundAction = false;
            var calledOnPostExecuteAction = false;
            var calledOnCancelledAction = false;

            var waitHandle = new ManualResetEvent(false);

            var task = AsyncTaskExecutor.Builder
                .SetOnPreExecuteAction(() => calledOnPreExecuteAction = true)
                .SetOnBackgroundAction((@null) =>
                {
                    Thread.Sleep(100);
                    calledOnBackgroundAction = true;
                    return null;
                })
                .SetOnPostExecuteAction((@null) =>
                {
                    calledOnPostExecuteAction = true;

                    waitHandle.Set();
                })
                .SetOnCancelledAction(() =>
                {
                    calledOnCancelledAction = true;

                    waitHandle.Set();
                })
                .Build();

            Assert.IsNotNull(task);

            task.Execute(null);

            waitHandle.WaitOne(10000);

            Assert.IsTrue(calledOnPreExecuteAction);
            Assert.IsTrue(calledOnBackgroundAction);
            Assert.IsTrue(calledOnPostExecuteAction);
            Assert.IsFalse(calledOnCancelledAction);
        }

        [Test]
        [Ignore]
        public void ShouldCancelExecutingTask()
        {
            var calledOnPreExecuteAction = false;
            var calledOnBackgroundAction = false;
            var calledOnPostExecuteAction = false;
            var calledOnCancelledAction = false;

            var waitHandle = new ManualResetEvent(false);

            var task = AsyncTaskExecutor.Builder
                .SetOnPreExecuteAction(() => calledOnPreExecuteAction = true)
                .SetOnBackgroundAction((@null) =>
                {
                    Thread.Sleep(100);
                    calledOnBackgroundAction = true;
                    return null;
                })
                .SetOnPostExecuteAction((@null) =>
                {
                    calledOnPostExecuteAction = true;

                    waitHandle.Set();
                })
                .SetOnCancelledAction(() =>
                {
                    calledOnCancelledAction = true;

                    waitHandle.Set();
                })
                .Build();

            Assert.IsNotNull(task);

            task.Execute(null);

            task.Cancel(true);

            waitHandle.WaitOne(10000);

            Assert.IsTrue(calledOnPreExecuteAction);
            Assert.IsTrue(calledOnBackgroundAction);
            Assert.IsFalse(calledOnPostExecuteAction);
            Assert.IsTrue(calledOnCancelledAction);
        }
    }
}