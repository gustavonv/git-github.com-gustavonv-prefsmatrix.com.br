using Android.App;
using Android.OS;
using System.Threading;

namespace PrefsMatrix
{
	[Activity(Theme = "@style/Theme.Splash", MainLauncher = true)]
	public class Splash : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Splash);
			ThreadPool.QueueUserWorkItem (o => LoadActivity ());

		}
		private void LoadActivity() {
			Thread.Sleep (1000); // Simulate a long pause
			RunOnUiThread (() => StartActivity (typeof(MainActivity)));
		}
	}
}



