
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
using Android.Support.V7.App;
using Android.Content.PM;

namespace PrefsMatrix
{	
	[Activity (Label = "Config", MainLauncher = true, Icon = "@mipmap/icon",Theme = "@style/Theme.AppCompat.Light",ScreenOrientation = ScreenOrientation.Portrait)]
	public class Config : ActionBarActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Config);
			SupportActionBar.SetDisplayHomeAsUpEnabled (true);
			SupportActionBar.SetDisplayShowHomeEnabled (true);

			// Create your application here
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{

			switch (item.ItemId)
			{
			case Android.Resource.Id.Home:
				var activity2 = new Intent (this, typeof(MainActivity));
				StartActivity (activity2);
				return true;		
			}
			return base.OnOptionsItemSelected(item);
		}
	}
}

