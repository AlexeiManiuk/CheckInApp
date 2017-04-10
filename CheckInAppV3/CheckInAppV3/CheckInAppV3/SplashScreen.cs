using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;

namespace CheckInAppV3
{
    [Activity(Theme ="@style/MyTheme.Splash", MainLauncher =true, NoHistory =true)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }
        protected override void OnResume()
        {
            base.OnResume();
            Task starupWork = new Task(()=> {
                Task.Delay(3000);
            });

            starupWork.ContinueWith(t => { StartActivity(new Intent(Application.Context, typeof(LoginActivity))); }, TaskScheduler.FromCurrentSynchronizationContext());
            starupWork.Start();
        }
    }
}

