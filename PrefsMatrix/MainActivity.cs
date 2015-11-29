using Android.App;
using Android.Widget;
using Android.OS;
using System.Json;
using System.Net;
using System.IO;
using Geolocator.Plugin;
using System.Threading.Tasks;
using System.Collections.Generic;
using Refractored.Xam.TTS;
using Refractored.Xam.TTS.Abstractions;
using System.Linq;
using System;
using Android.Locations;
using Android.Support.V7.App;
using Android.Views;
using Android.Content;
using Android.Content.PM;

namespace PrefsMatrix
{
	public class DataVc
	{
		public string Speed { get; set; }
		public string Msg { get; set; }
	}

	[Activity (Label = "PrefsMatrix",Icon = "@mipmap/icon",Theme = "@style/Theme.AppCompat.Light",ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : ActionBarActivity
	{
		static CrossLocale? locale = null;
		string _newSpeed =string.Empty;
		string _lat =string.Empty;
		string _lon =string.Empty;
		string _userId =string.Empty;
		string _msg =string.Empty;
		string _endereco =string.Empty;

		string textSpeech = string.Empty;

		protected override void OnCreate (Bundle savedInstanceState)
		{		
			base.OnCreate (savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			// Get our button from the layout resource,
		
			TextView txtVelocidadeElement = (TextView) FindViewById(Resource.Id.txtVelocidade);;

			txtVelocidadeElement.Touch += delegate(object sender, View.TouchEventArgs e) {
				switch (e.Event.Action) {
				case MotionEventActions.Down:
					Toast.MakeText (this, "Carregando velocidade....", ToastLength.Long).Show ();
					var locator = CrossGeolocator.Current;

					IEnumerable<CrossLocale> list = CrossTextToSpeech.Current.GetInstalledLanguages ();

					// Get our button from the layout resource,
					// and attach an event to it
					string lat = "43,225";
					string lon = "43,225";

					// Get the latitude and longitude entered by the user and create a query.
					string url = "http://jbluecrm.com.br:8080/matrixprefs/rideme.xhtml?userid=34&" +
					             lat +
					             "&lon=" +
					             lon +
					             "&speed=65&";



					// parse the results, then update the screen:
					//JsonValue json = FetchWeatherAsync(url);

					Random r = new Random ();
					int number = r.Next (30, 70);

					_newSpeed = number.ToString ();
					/*_newSpeed = json["newspeed"];
			_lat = json["lat"];
			_lon =json["lon"];
			_userId =json["userId"];
			_msg =json["msg"];
			_endereco =json["endereco"];*/

					textSpeech = string.Format ("{0} KM DE VELOCIDADE PRA ESTA VIA", _newSpeed);

					TextView lblNewSpeed = (TextView)FindViewById (Resource.Id.txtVelocidade);
					lblNewSpeed.Text = _newSpeed;

					var locales = CrossTextToSpeech.Current.GetInstalledLanguages ();
					var items = locales.Select (a => a.ToString ()).ToArray ();
					string _langague = items.FirstOrDefault (a => a.Equals ("pt-BR")).ToString ();

					locale = new CrossLocale { Language = _langague };//fine for iOS/WP

					CrossTextToSpeech.Current.Speak (textSpeech, false, locale);
					base.OnStop ();
					break;

				case MotionEventActions.Up:
					
					break;

				default:

					break;
				}

				

			
			};
	}
		public override bool OnPrepareOptionsMenu (IMenu menu)
		{			

			return base.OnPrepareOptionsMenu (menu);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			this.MenuInflater.Inflate(Resource.Menu.main_menu, menu);

			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId) {
			case Resource.Id.configs:
				var activity2 = new Intent (this, typeof(Config));
				StartActivity (activity2);
				return true;
			}
			return base.OnOptionsItemSelected (item);
		}



		// Gets weather data from the passed URL.
		public JsonValue FetchWeatherAsync(string url)
		{
			// Create an HTTP web request using the URL:
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
			request.ContentType = "application/json";
			request.Method = "GET";

			// Send the request to the server and wait for the response:
			using (WebResponse response = request.GetResponse ())
			{
				// Get a stream representation of the HTTP web response:
				using (Stream stream = response.GetResponseStream ())
				{
					// Use this stream to build a JSON document object:
					JsonValue jsonDoc =  JsonObject.Load(stream);

					// Return the JSON document:
					return jsonDoc;
				}
			}
		}
}
}
